using Harmic.Models;
using Harmic.Utilities;
using Harmic.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizePermission("Productcategories", "Index")]
    public class ProductcategoriesController : Controller
    {
        private readonly HarmicContext _context;

        public ProductcategoriesController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/Productcategories
        public async Task<IActionResult> Index()
        {
            // Permission is checked by [AuthorizePermission] attribute
            return View(await _context.TbProductcategories.Include(i => i.TbProducts).OrderBy(i => i.Position).ToListAsync());
        }

        // GET: Admin/Productcategories/Details/5
        [AuthorizePermission("Productcategories", "Index")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProductcategory = await _context.TbProductcategories
                .FirstOrDefaultAsync(m => m.CategoryProductId == id);
            if (tbProductcategory == null)
            {
                return NotFound();
            }

            return View(tbProductcategory);
        }

        // GET: Admin/Productcategories/Create
        [AuthorizePermission("Productcategories", "Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Productcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizePermission("Productcategories", "Create")]
        public async Task<IActionResult> Create([Bind("CategoryProductId,Title,Alias,Description,Icon,Position,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] TbProductcategory tbProductcategory)
        {
            tbProductcategory.CreatedDate = DateTime.Now;
            tbProductcategory.CreatedBy = Function._FullName;

            tbProductcategory.Position = _context.TbProductcategories.Max(x => x.Position) + 1;

            if (ModelState.IsValid)
            {
                _context.TbProductcategories.Add(tbProductcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductcategory);
        }

        // GET: Admin/Productcategories/Edit/5
        [AuthorizePermission("Productcategories", "Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProductcategory = await _context.TbProductcategories.FindAsync(id);
            if (tbProductcategory == null)
            {
                return NotFound();
            }
            return View(tbProductcategory);
        }

        // POST: Admin/Productcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizePermission("Productcategories", "Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryProductId,Title,Alias,Description,Icon,Position,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] TbProductcategory tbProductcategory)
        {
            if (id != tbProductcategory.CategoryProductId)
            {
                return NotFound();
            }

            tbProductcategory.ModifiedDate = DateTime.Now;
            tbProductcategory.ModifiedBy = Function._FullName;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProductcategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductcategoryExists(tbProductcategory.CategoryProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductcategory);
        }

        [AuthorizePermission("Productcategories", "Edit")]
        public async Task<IActionResult> MoveUp(int id)
        {
            var category = await _context.TbProductcategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            if (category.Position > 1)
            {
                var previousCategory = await _context.TbProductcategories
                    .Where(i => i.Position == category.Position - 1)
                    .FirstOrDefaultAsync();
                category.Position -= 1;
                if (previousCategory != null)
                {
                    previousCategory.Position += 1;
                    _context.Update(previousCategory);
                }
                _context.TbProductcategories.Update(category);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }

        [AuthorizePermission("Productcategories", "Edit")]
        public async Task<IActionResult> MoveDown(int id)
        {
            var category = await _context.TbProductcategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            if (category.Position < _context.TbProductcategories.Max(i => i.Position))
            {
                var nextCategory = await _context.TbProductcategories
                    .Where(i => i.Position == category.Position + 1)
                    .FirstOrDefaultAsync();
                category.Position += 1;
                if (nextCategory != null)
                {
                    nextCategory.Position -= 1;
                    _context.Update(nextCategory);
                }
                _context.TbProductcategories.Update(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [AuthorizePermission("Productcategories", "Edit")]
        public async Task<IActionResult> ChangeShow(int id)
        {
            var category = await _context.TbProductcategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            category.IsActive = !category.IsActive;
            _context.TbProductcategories.Update(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Admin/Productcategories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizePermission("Productcategories", "Delete")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var tbProductcategory = await _context.TbProductcategories
                    .Include(c => c.TbProducts)
                    .FirstOrDefaultAsync(c => c.CategoryProductId == id);
                
                if (tbProductcategory == null)
                {
                    Function._Message = "Không tìm thấy danh mục cần xóa";
                    return RedirectToAction(nameof(Index));
                }

                // Kiểm tra xem có sản phẩm nào đang sử dụng danh mục này không
                if (tbProductcategory.TbProducts != null && tbProductcategory.TbProducts.Any())
                {
                    Function._Message = $"Không thể xóa danh mục \"{tbProductcategory.Title}\" vì còn {tbProductcategory.TbProducts.Count()} sản phẩm đang sử dụng. Vui lòng xóa hoặc chuyển các sản phẩm sang danh mục khác trước.";
                    return RedirectToAction(nameof(Index));
                }

                // Xóa danh mục
                _context.TbProductcategories.Remove(tbProductcategory);
                await _context.SaveChangesAsync();

                Function._Message = "Đã xóa danh mục thành công";
            }
            catch (Exception ex)
            {
                Function._Message = $"Lỗi khi xóa danh mục: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TbProductcategoryExists(int id)
        {
            return _context.TbProductcategories.Any(e => e.CategoryProductId == id);
        }
    }
}
