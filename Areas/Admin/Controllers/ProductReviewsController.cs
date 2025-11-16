using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductReviewsController : Controller
    {
        private readonly HarmicContext _context;

        public ProductReviewsController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductReviews
        public async Task<IActionResult> Index()
        {
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Home");
            }

            var reviews = await _context.TbProductreviews
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();

            return View(reviews);
        }

        // POST: Admin/ProductReviews/ChangeStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Home");
            }

            var review = await _context.TbProductreviews.FindAsync(id);
            if (review == null)
            {
                Function._Message = "Không tìm thấy đánh giá";
                return RedirectToAction(nameof(Index));
            }

            review.IsActive = !review.IsActive;
            _context.TbProductreviews.Update(review);
            await _context.SaveChangesAsync();

            Function._Message = review.IsActive ? "Đã duyệt đánh giá" : "Đã ẩn đánh giá";
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/ProductReviews/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Home");
            }

            try
            {
                var review = await _context.TbProductreviews.FindAsync(id);
                if (review == null)
                {
                    Function._Message = "Không tìm thấy đánh giá cần xóa";
                    return RedirectToAction(nameof(Index));
                }

                _context.TbProductreviews.Remove(review);
                await _context.SaveChangesAsync();

                Function._Message = "Đã xóa đánh giá thành công";
            }
            catch (Exception ex)
            {
                Function._Message = $"Lỗi khi xóa đánh giá: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

