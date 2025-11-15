using Microsoft.AspNetCore.Mvc;
using Harmic.Utilities;
using Harmic.Models;
using AspNetCoreGeneratedDocument;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Controllers
{
    public class WishlistController : Controller
    {
        private readonly HarmicContext _context;

        public WishlistController(HarmicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!Function.isLogin())
            {
                Function._ReturnUrl = Request.Path;
                return RedirectToAction("Index", "Login");
            }


            var wishlists = _context.TbWishlishes.Where(m => m.AccountId == Function._AccountId).Include(i => i.Product).ToList();

            return View(wishlists);
        }

        public IActionResult AddOrRemoveWishlist(int id, string Url)
        {
            if (!Function.isLogin())
            {
                Function._ReturnUrl = Url;
                return RedirectToAction("Index", "Login");
            }

            var product = _context.TbProducts.FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                Function._Message = "Lỗi";
                return Redirect(Url);
            }

            var wishlist = _context.TbWishlishes.FirstOrDefault(m => m.ProductId == id && m.AccountId == Function._AccountId);
            if (wishlist != null)
            {
                _context.TbWishlishes.Remove(wishlist);
                _context.SaveChanges();
                Function._Message = "Đã xóa sản phẩm khỏi danh sách yêu thích";
                return Redirect(Url);
            }
            else
            {
                TbWishlish wishlish = new TbWishlish
                {
                    ProductId = id,
                    AccountId = Function._AccountId,
                };
                _context.TbWishlishes.Add(wishlish);
                _context.SaveChanges();
                Function._Message = "Đã thêm sản phẩm vào danh sách yêu thích";

                return Redirect(Url);
            }
        }

    }
}