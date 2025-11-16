using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;
using Harmic.Utilities;
using Harmic.Attributes;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizePermission("Categories", "Index")]
    public class CategoriesController : Controller
    {
        private readonly HarmicContext _context;
        public CategoriesController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }
            var categories = _context.TbCategories.Include(c => c.TbBlogs).OrderBy(i=>i.Position).ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            if (!Function.canAccessAdmin())
            {
                // Nếu là AJAX request, trả về JSON
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Bạn không có quyền truy cập" });
                }
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(TbCategory category)
        {
            category.Alias = Function.TitleSlugGenerationAlias(category.Title);
            category.CreatedDate = DateTime.Now;
            category.CreatedBy = Function._FullName;
            category.Position = _context.TbCategories.Count() + 1;

            _context.TbCategories.Add(category);
            _context.SaveChanges();

            return View("Index");
        }

        public IActionResult Edit(int id)
        {
            if (!Function.canAccessAdmin())
            {
                // Nếu là AJAX request, trả về JSON
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Bạn không có quyền truy cập" });
                }
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }
            var category = _context.TbCategories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(TbCategory category)
        {
            var check = _context.TbCategories.Find(category.CategoryId);
            if (check == null)
            {
                return NotFound();
            }
            _context.TbCategories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult MoveUp(int id)
        {
            if (!Function.canAccessAdmin())
            {
                // Nếu là AJAX request, trả về JSON
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Bạn không có quyền truy cập" });
                }
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }
            var category = _context.TbCategories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            if(category.Position > 1)
            {
                var previousCategory = _context.TbCategories.FirstOrDefault(c => c.Position == category.Position - 1);
                previousCategory.Position++;
                category.Position--;
                _context.TbCategories.Update(previousCategory);
                _context.TbCategories.Update(category);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        public IActionResult MoveDown(int id) { 
            
            if (!Function.canAccessAdmin())
            {
                // Nếu là AJAX request, trả về JSON
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Bạn không có quyền truy cập" });
                }
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }
            var category = _context.TbCategories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            var nextCategory = _context.TbCategories.FirstOrDefault(c => c.Position == category.Position + 1);
            if (nextCategory != null)
            {
                nextCategory.Position--;
                category.Position++;
                _context.TbCategories.Update(nextCategory);
                _context.TbCategories.Update(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}
