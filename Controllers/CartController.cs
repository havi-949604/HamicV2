using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Controllers
{
    public class CartController : Controller
    {
        private readonly HarmicContext _context;

        public CartController(HarmicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!Function.isLogin())
            {

                return RedirectToAction("Index", "Login");
            }
            var cartItems = _context.TbCarts.Where(c => c.IdCustomer == Function._AccountId).Include(i => i.IdProductNavigation).ToList();

            int TotalPrice = 0;
            foreach (var item in cartItems)
            {

                if (item.IdProductNavigation.PriceSale == 0)
                {
                    TotalPrice += (int)(item.IdProductNavigation.Price * item.Quantity);
                }
                else
                {
                    TotalPrice += (int)(item.IdProductNavigation.PriceSale * item.Quantity);

                }
            }

            ViewBag.TotalPrice = TotalPrice;

            return View(cartItems);
        }

        public IActionResult addCart(int id, string Url, int Quantity = 1)
        {

            if (id == null || _context.TbProducts == null)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });
                }
                return NotFound();
            }

            if (!Function.isLogin())
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập!", redirect = "/Login" });
                }
                Function._Message = "Vui lòng đăng nhập!";
                Function._ReturnUrl = Url;
                return RedirectToAction("Index", "Login");
            }
            var check = _context.TbProducts.FirstOrDefault(m => m.ProductId == id);
            if (check == null)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });
                }
                return NotFound();
            }
            if (Quantity < 1)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Số lượng không hợp lệ!" });
                }
                Function._Message = "Số lượng không hợp lệ!";
                return Redirect(Url);
            }

            var existingCartItem = _context.TbCarts.FirstOrDefault(c => c.IdProduct == id && c.IdCustomer == Function._AccountId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += Quantity;
                _context.TbCarts.Update(existingCartItem);
                _context.SaveChanges();
            }
            else
            {

                TbCart miniCart = new TbCart();
                miniCart.IdProduct = id;
                miniCart.Quantity = Quantity;
                miniCart.IdCustomer = Function._AccountId;
                _context.TbCarts.Add(miniCart);
                _context.SaveChanges();
            }
            
            // Nếu là AJAX request, trả về JSON
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, message = $"Đã thêm {check.Title} vào giỏ hàng!" });
            }
            
            // Nếu không phải AJAX, giữ nguyên hành vi cũ
            Function._Message = $"Đã thêm {check.Title} vào giỏ hàng!";
            return Redirect(Url);

        }

        public async Task<IActionResult> RemoveFromCart(int cartId)
        {
            if (!Function.isLogin())
            {
                return await Task.FromResult<IActionResult>(RedirectToAction("Index", "Login"));
            }
            var cartItem = await _context.TbCarts.FindAsync(cartId);
            if (cartItem != null && cartItem.IdCustomer == Function._AccountId)
            {
                _context.TbCarts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> ChangeQuantity(int cartId, int quantity)
        {
            if (!Function.isLogin())
            {
                return await Task.FromResult<IActionResult>(RedirectToAction("Index", "Login"));
            }
            var cartItem = await _context.TbCarts.FindAsync(cartId);
            if (cartItem != null && cartItem.IdCustomer == Function._AccountId)
            {
                cartItem.Quantity = quantity;
                _context.Update(cartItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
