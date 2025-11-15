using Harmic.Models;
using Harmic.Utilities;
using Harmic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Controllers
{
    public class BlogController : Controller
    {
        private readonly HarmicContext _context;
        public BlogController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index(BlogSearchViewModel search)
        {
            const int pageSize = 10;

            if (search.Page < 1)
            {
                search.Page = 1;
            }

            IQueryable<TbBlog> blogsQuery = _context.TbBlogs.Include(i => i.Category).Where(i => i.IsActive);

            if (search.CategoryId > 0)
            {
                blogsQuery = blogsQuery.Where(i => i.CategoryId == search.CategoryId);
            }
            if (!string.IsNullOrEmpty(search.SearchText))
            {
                blogsQuery = blogsQuery.Where(i => i.Title.Contains(search.SearchText) || i.Description.Contains(search.SearchText) || i.CreatedBy.Contains(search.SearchText));
            }

            if (search.SortBy == "NewFirst")
            {
                blogsQuery = blogsQuery.OrderByDescending(i => i.CreatedDate);
            }
            else if (search.SortBy == "ProminentFirst")
            {
                blogsQuery = blogsQuery.OrderByDescending(i => i.TbBlogcomments.Count());
            }
            else
            {
                search.SortBy = "NewFirst";
                blogsQuery = blogsQuery.OrderByDescending(i => i.CreatedDate);
            }
            int totalBlogs = blogsQuery.Count();

            int pageNum = (int)Math.Ceiling((double)totalBlogs / pageSize);
            ViewBag.PageNum = pageNum;

            blogsQuery = blogsQuery.Skip((search.Page - 1) * pageSize).Take(pageSize);

            ViewBag.Categories = _context.TbCategories.OrderBy(i => i.Position).ToList();
            ViewBag.Blogs = blogsQuery.ToList();
            return View(search);

        }

        [Route("/blog/{alias}-{id}.html")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbBlogs == null)
            {
                return NotFound();
            }

            var blog = await _context.TbBlogs.Include(m => m.TbBlogcomments).Where(m => m.BlogId == id).FirstOrDefaultAsync(m => m.BlogId == id);

            if (blog == null)
            {
                return NotFound();
            }
            ;

            return View(blog);
        }

        public IActionResult Login(int id)
        {

            var check = _context.TbBlogs.FirstOrDefault(i => i.BlogId == id);
            string url = $"/blog/{check.Alias}-{id}.html";
            Function._ReturnUrl = url;

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Comment(TbBlogcomment blogComment, int id)
        {
            var check = _context.TbBlogs.FirstOrDefault(i => i.BlogId == id);
            blogComment.CreatedDate = DateTime.Now;
            blogComment.IsActive = true;
            _context.Add(blogComment);
            _context.SaveChanges();

            string url = $"/blog/{check.Alias}-{blogComment.BlogId}.html";
            return Redirect(url);
        }
    }
}
