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
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập!", redirect = "/Login" });
                }
                Function._ReturnUrl = Url;
                return RedirectToAction("Index", "Login");
            }

            var product = _context.TbProducts.FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });
                }
                Function._Message = "Lỗi";
                return Redirect(Url);
            }

            var wishlist = _context.TbWishlishes.FirstOrDefault(m => m.ProductId == id && m.AccountId == Function._AccountId);
            if (wishlist != null)
            {
                _context.TbWishlishes.Remove(wishlist);
                _context.SaveChanges();
                
                // Nếu là AJAX request, trả về JSON
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = $"Đã xóa {product.Title} khỏi danh sách yêu thích", isInWishlist = false });
                }
                
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
                
                // Nếu là AJAX request, trả về JSON
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = $"Đã thêm {product.Title} vào danh sách yêu thích", isInWishlist = true });
                }
                
                Function._Message = "Đã thêm sản phẩm vào danh sách yêu thích";
                return Redirect(Url);
            }
        }

        public IActionResult AddToWishlist(int id, string Url)
        {
            if (!Function.isLogin())
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập!", redirect = "/Login" });
                }
                Function._ReturnUrl = Url;
                return RedirectToAction("Index", "Login");
            }

            var product = _context.TbProducts.FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });
                }
                Function._Message = "Lỗi";
                return Redirect(Url);
            }

            var wishlist = _context.TbWishlishes.FirstOrDefault(m => m.ProductId == id && m.AccountId == Function._AccountId);
            if (wishlist == null)
            {
                TbWishlish wishlish = new TbWishlish
                {
                    ProductId = id,
                    AccountId = Function._AccountId,
                };
                _context.TbWishlishes.Add(wishlish);
                _context.SaveChanges();
                
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = $"Đã thêm {product.Title} vào danh sách yêu thích", isInWishlist = true });
                }
                
                Function._Message = "Đã thêm sản phẩm vào danh sách yêu thích";
            }
            else
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = "Sản phẩm đã có trong danh sách yêu thích", isInWishlist = true });
                }
            }

            return Redirect(Url);
        }

        public IActionResult RemoveFromWishlist(int id, string Url = null)
        {
            if (!Function.isLogin())
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập!", redirect = "/Login" });
                }
                if (!string.IsNullOrEmpty(Url))
                {
                    Function._ReturnUrl = Url;
                }
                return RedirectToAction("Index", "Login");
            }

            var product = _context.TbProducts.FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });
                }
                Function._Message = "Lỗi";
                if (!string.IsNullOrEmpty(Url))
                {
                    return Redirect(Url);
                }
                return RedirectToAction("Index");
            }

            var wishlist = _context.TbWishlishes.FirstOrDefault(m => m.ProductId == id && m.AccountId == Function._AccountId);
            if (wishlist != null)
            {
                _context.TbWishlishes.Remove(wishlist);
                _context.SaveChanges();
                
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, message = $"Đã xóa {product.Title} khỏi danh sách yêu thích", isInWishlist = false });
                }
                
                Function._Message = "Đã xóa sản phẩm khỏi danh sách yêu thích";
            }

            if (!string.IsNullOrEmpty(Url))
            {
                return Redirect(Url);
            }
            return RedirectToAction("Index");
        }

    }
}