using Harmic.Models;
using Harmic.Services.Momo;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;
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
                var newOder = new TbCheckout
                {
                    FullName = Function._FullName,
                    OrderId = RequestQuery["orderId"],
                    OrderInfo = RequestQuery["orderInfo"],
                    Amount = int.Parse(RequestQuery["amount"]),
                    DatePaid = DateTime.Now,
                    Method = "Momo"
                };
                _context.TbCheckouts.Add(newOder);
                await _context.SaveChangesAsync();

            }
            else
            {
                Function._Message = $"Thanh toán thất bại, mã lỗi {RequestQuery["errorCode"]}";
                Console.WriteLine(RequestQuery["message"]);
                return RedirectToAction("Index", "Cart");
            }

            var cartItems = _context.TbCarts.Where(c => c.IdCustomer == Function._AccountId).ToList();
            _context.TbCarts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            Function._Message = "Thanh toán thành công";

            return RedirectToAction("Index", "Cart");
        }

    }
}
