using Harmic.Models;
using Harmic.Services.Momo;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Harmic.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IMomoService _momoService;
        private readonly HarmicContext _context;

        public CheckoutController(IMomoService momoService, HarmicContext context)
        {
            _momoService = momoService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PaymentCallBackMomo()
        {
            var respone = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            var RequestQuery = HttpContext.Request.Query;

            if (RequestQuery["errorCode"] == "0")
            {
                // Lưu thông tin thanh toán
                var newOder = new TbCheckout
                {
                    FullName = Function._FullName,
                    OrderId = RequestQuery["orderId"].ToString(),
                    OrderInfo = RequestQuery["orderInfo"].ToString(),
                    Amount = int.Parse(RequestQuery["amount"].ToString() ?? "0"),
                    DatePaid = DateTime.Now,
                    Method = "Momo"
                };
                _context.TbCheckouts.Add(newOder);

                // Lấy thông tin khách hàng
                var customer = await _context.TbCustomers.FindAsync(Function._AccountId);
                
                // Lấy giỏ hàng với thông tin sản phẩm
                var cartItems = _context.TbCarts
                    .Where(c => c.IdCustomer == Function._AccountId)
                    .Include(c => c.IdProductNavigation)
                    .ToList();

                if (cartItems != null && cartItems.Count > 0)
                {
                    // Tạo mã đơn hàng
                    string orderCode = "ORD" + DateTime.Now.ToString("yyyyMMddHHmmss") + Function._AccountId.ToString().PadLeft(4, '0');
                    
                    // Tính tổng số lượng và tổng tiền
                    int totalQuantity = cartItems.Sum(c => c.Quantity ?? 0);
                    int totalAmount = 0;
                    foreach (var item in cartItems)
                    {
                        var product = item.IdProductNavigation;
                        if (product == null) continue;
                        decimal price = (product.PriceSale ?? 0) > 0 ? (decimal)(product.PriceSale ?? 0) : (decimal)(product.Price ?? 0);
                        totalAmount += (int)(price * (item.Quantity ?? 0));
                    }

                    // Tạo đơn hàng
                    var order = new TbOrder
                    {
                        Code = orderCode,
                        CustomerName = Function._FullName,
                        Phone = customer?.Phone,
                        Address = null, // Có thể thêm form nhập địa chỉ giao hàng sau
                        TotalAmount = totalAmount,
                        Quanlity = totalQuantity,
                        OrderStatusId = 1, // 1 = Đang xử lý (có thể cần kiểm tra lại trong database)
                        CreatedDate = DateTime.Now,
                        CustomerId = Function._AccountId
                    };
                    _context.TbOrders.Add(order);
                    await _context.SaveChangesAsync(); // Lưu để lấy OrderId

                    // Tạo chi tiết đơn hàng và cập nhật tồn kho
                    foreach (var cartItem in cartItems)
                    {
                        var product = cartItem.IdProductNavigation;
                        if (product == null) continue;
                        
                        decimal price = (product.PriceSale ?? 0) > 0 ? (decimal)(product.PriceSale ?? 0) : (decimal)(product.Price ?? 0);
                        int quantity = cartItem.Quantity ?? 0;

                        // Tạo chi tiết đơn hàng
                        var orderDetail = new TbOrderdetail
                        {
                            OrderId = order.OrderId,
                            ProductId = product.ProductId,
                            Price = price,
                            Quantity = quantity
                        };
                        _context.TbOrderdetails.Add(orderDetail);

                        // Cập nhật số lượng tồn kho (giảm số lượng đã bán)
                        if (product.UnitInStock.HasValue)
                        {
                            // Đảm bảo tồn kho không âm
                            int currentStock = product.UnitInStock.Value;
                            int newStock = Math.Max(0, currentStock - quantity);
                            product.UnitInStock = newStock;
                            _context.TbProducts.Update(product);
                        }
                        else
                        {
                            // Nếu tồn kho là null, đặt về 0 sau khi trừ
                            int newStock = Math.Max(0, 0 - quantity);
                            product.UnitInStock = newStock;
                            _context.TbProducts.Update(product);
                        }
                    }

                    // Xóa giỏ hàng
                    _context.TbCarts.RemoveRange(cartItems);
                }

                await _context.SaveChangesAsync();
                Function._Message = "Thanh toán thành công";
            }
            else
            {
                Function._Message = $"Thanh toán thất bại, mã lỗi {RequestQuery["errorCode"]}";
                Console.WriteLine(RequestQuery["message"]);
                return RedirectToAction("Index", "Cart");
            }

            return RedirectToAction("Index", "Cart");
        }

    }
}
