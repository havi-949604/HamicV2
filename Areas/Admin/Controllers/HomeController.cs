using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!Function.isLogin())
            {
                Function._Message = "Bạn cần phải đăng nhập";
                return Redirect("/Login");
            }
            else if (Function._RoleId == 1)
            {
                Function._Message = "Bạn không có quyền truy cập vào trang này";
                return Redirect("/Login");
            }

            return View();
        }

    }
}
