using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Areas.Admin.Controllers
{
    public class FileManagerController : Controller
    {
        [Area("Admin")]
        //[Route("/Admin/file-manager")]
        public IActionResult Index()
        {
            if (Function.isLogin())
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

    }
}
