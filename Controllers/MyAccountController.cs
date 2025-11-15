using Microsoft.AspNetCore.Mvc;

namespace Harmic.Controllers
{
    public class MyAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
