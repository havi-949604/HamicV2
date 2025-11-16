using Harmic.Utilities;
using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly HarmicContext _context;

        public HomeController(HarmicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!Function.isLogin())
            {
                Function._Message = "Bạn cần phải đăng nhập";
                return Redirect("/Login");
            }
            else if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập vào trang này";
                return Redirect("/Home");
            }

            // Tính tổng số lượng tồn kho của tất cả sản phẩm
            var products = _context.TbProducts.ToList();
            int totalInventory = 0;
            
            foreach (var product in products)
            {
                int stock = product.UnitInStock ?? 0;
                totalInventory += stock;
            }

            // Đảm bảo ViewBag luôn có giá trị
            ViewBag.TotalInventory = totalInventory;

            return View();
        }

    }
}
