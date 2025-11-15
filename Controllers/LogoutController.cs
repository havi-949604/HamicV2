using Microsoft.AspNetCore.Mvc;
using Harmic.Utilities;
namespace Harmic.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            Function._FullName = string.Empty;
            Function._Message = string.Empty;
            Function._Email = string.Empty;
            Function._AccountId = 0;
            Function._RoleId = 0;
            return RedirectToAction("Index", "Login");
        }
    }
}
