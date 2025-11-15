using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Harmic.Controllers
{
    public class HomeController : Controller
    {
        private readonly HarmicContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(HarmicContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        } 

        public IActionResult Index()
        {
            ViewBag.Sliders = _context.TbHomesliders.Where(i=>i.IsActive).OrderBy(i=>i.Position).ToList();
            ViewBag.productCategories = _context.TbProductcategories.ToList();
            ViewBag.productNew = _context.TbProducts.Where(m => m.IsNew).ToList();
            ViewBag.commentBlogs = _context.TbBlogcomments.ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
