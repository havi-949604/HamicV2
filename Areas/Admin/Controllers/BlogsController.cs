using Microsoft.AspNetCore.Mvc;
using Harmic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Harmic.Utilities;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly HarmicContext _context;

        public BlogsController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var blogs = _context.TbBlogs.Include(b => b.TbBlogcomments)
                 .Include(b => b.Category)
                 .Include(b => b.Account).OrderByDescending(b => b.CreatedDate)
                 .ToList();
            return View(blogs);
        }

        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_context.TbCategories.OrderBy(i => i.Position), "CategoryId", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create(TbBlog blog)
        {
            blog.AccountId = Function._AccountId;
            blog.CreatedDate = DateTime.Now;
            blog.Alias = Function.TitleSlugGenerationAlias(blog.Title);
            blog.CreatedBy = Function._FullName;

            _context.TbBlogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var blog = _context.TbBlogs.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["Category"] = new SelectList(_context.TbCategories.OrderBy(i => i.Position), "CategoryId", "Title");
            return View(blog);
        }

        [HttpPost]
        public IActionResult Edit(TbBlog blog)
        {
            blog.ModifiedDate = DateTime.Now;
            blog.ModifiedBy = Function._FullName;

            _context.TbBlogs.Update(blog);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
