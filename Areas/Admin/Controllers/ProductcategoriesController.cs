using Harmic.Models;
using Harmic.Utilities;
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
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Home");
            }
            return View(await _context.TbProductcategories.Include(i => i.TbProducts).OrderBy(i => i.Position).ToListAsync());
        }

        // GET: Admin/Productcategories/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Productcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        private bool TbProductcategoryExists(int id)
        {
            return _context.TbProductcategories.Any(e => e.CategoryProductId == id);
        }
    }
}
