using Microsoft.AspNetCore.Mvc;
using Harmic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Harmic.Utilities;
using Harmic.Attributes;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizePermission("Blogs", "Index")]
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
            // Tìm AccountId từ email hoặc username của người dùng đã đăng nhập
            int? accountId = null;
            if (!string.IsNullOrEmpty(Function._Email))
            {
                var account = _context.TbAccounts.FirstOrDefault(a => a.Email == Function._Email);
                if (account != null)
                {
                    accountId = account.AccountId;
                }
            }
            
            // Nếu không tìm thấy bằng email, thử tìm bằng username
            if (accountId == null && !string.IsNullOrEmpty(Function._FullName))
            {
                var account = _context.TbAccounts.FirstOrDefault(a => a.Username == Function._FullName);
                if (account != null)
                {
                    accountId = account.AccountId;
                }
            }

            blog.AccountId = accountId;
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
