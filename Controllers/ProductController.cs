using Harmic.Models;
using Harmic.Utilities;
using Harmic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Controllers
{
    public class ProductController : Controller
    {
        private readonly HarmicContext _context;

        public ProductController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index(ProductSearchViewModel search)
        {
            const int pageSize = 10;

            if (search.Page < 1)
            {
                search.Page = 1;
            }

            IQueryable<TbProduct> productsQuery = _context.TbProducts.Include(i => i.CategoryProduct).Where(i => i.IsActive && i.CategoryProduct.IsActive);
            if (search.CategoryId > 0)
            {
                productsQuery = productsQuery.Where(i => i.CategoryProductId == search.CategoryId);
            }
            if (!string.IsNullOrEmpty(search.SearchText))
            {
                productsQuery = productsQuery.Where(i => i.Title.Contains(search.SearchText) || i.Description.Contains(search.SearchText) || i.CreatedBy.Contains(search.SearchText));
            }
            if (search.onSale)
            {
                productsQuery = productsQuery.Where(i => i.PriceSale > 0);
            }
            if (search.isNew)
            {
                productsQuery = productsQuery.Where(i => i.IsNew);
            }
            if (search.inStock)
            {
                productsQuery = productsQuery.Where(i => i.UnitInStock > 0);
            }
            int totalProducts = productsQuery.Count();

            ViewBag.TotalProducts = totalProducts;
            int pageNum = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.PageNum = pageNum;

            productsQuery = productsQuery.OrderByDescending(i => i.PriceSale).Skip((search.Page - 1) * pageSize).Take(pageSize);

            ViewBag.Products = productsQuery.ToList();
            return View(search);
        }

        [Route("/product/{alias}-{id}.html")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }
            var product = await _context.TbProducts.Include(i => i.TbProductreviews).ThenInclude(i=>i.Customer).Include(i => i.CategoryProduct).FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.productReview = _context.TbProductreviews.Where(i => i.ProductId == id && i.IsActive && i.CustomerId != Function._AccountId).Include(i=>i.Customer).ToList();
            ViewBag.productRelated = _context.TbProducts.Where(i => i.ProductId != id && i.CategoryProductId == product.CategoryProductId).Take(5).OrderByDescending(i => i.ProductId).ToList();
            return View(product);
        }

        [HttpPost]
        public IActionResult Review(int ProductId, string Review, int Star)
        {
            var product = _context.TbProducts.FirstOrDefault(i => i.ProductId == ProductId);

            if (product == null)
            {
                Function._Message = "Lỗi!";
                return RedirectToAction("Index", "Home");
            }

            string url = $"/product/{product.Alias}-{product.ProductId}.html";

            if(Star < 1 || Star > 5)
            {
                Function._Message = "Có lỗi xảy ra! Vui lòng đánh giá lại";
                return Redirect(url);
            }

            if (!Function.isLogin())
            {
                Function._ReturnUrl = url;
                return RedirectToAction("Index", "Login");
            }
            
            var check = _context.TbProductreviews.FirstOrDefault(i => i.ProductId == ProductId && i.CustomerId == Function._AccountId);
            if (check == null)
            {
                TbProductreview review = new TbProductreview();
                review.ProductId = ProductId;
                review.CustomerId = Function._AccountId;
                review.CreatedDate = DateTime.Now;
                review.Detail = Review;
                review.Star = Star;
                review.IsActive = true;

                _context.TbProductreviews.Add(review);
                _context.SaveChanges();
                Function._Message = "Đã lưu đánh giá của bạn!";
            }
            else if(check != null && check.IsActive == false)
            {
                check.Detail = Review;
                check.Star = Star;
                check.IsActive = true;
                check.CreatedDate = DateTime.Now;

                _context.TbProductreviews.Update(check);
                _context.SaveChanges();
                Function._Message = "Đã lưu đánh giá của bạn!";
            }
            else
            {
                Function._Message = "Bạn đã đánh giá sản phẩm này rồi!";
            }
            return Redirect(url);

        }

        public IActionResult RemoveReview(int id)
        {
            var product = _context.TbProducts.FirstOrDefault(i => i.ProductId == id);

            if (product == null)
            {
                Function._Message = "Lỗi!";
                return RedirectToAction("Index", "Home");
            }

            string url = $"/product/{product.Alias}-{product.ProductId}.html";

            var check = _context.TbProductreviews.FirstOrDefault(i => i.ProductId == id && i.CustomerId == Function._AccountId);
            if (check != null && check.IsActive == true)
            {
                check.IsActive = false;
                _context.TbProductreviews.Update(check);
                _context.SaveChanges();
                Function._Message = "Đã xóa đánh giá của bạn!";
            }
            else
            {
                Function._Message = "Không tìm thấy đánh giá!";
            }
            return Redirect(url);
        }
    }
}
