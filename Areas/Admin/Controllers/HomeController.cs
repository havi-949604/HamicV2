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

            // Kiểm tra xem menu Orders đã được kích hoạt chưa
            var hasOrdersMenu = _context.TbAdminmenus.Any(m => m.Alias == "Orders" && m.IsActive);
            ViewBag.HasOrdersMenu = hasOrdersMenu;

            return View();
        }

        // Action để kích hoạt menu "Quản lý đơn hàng"
        public IActionResult EnableOrdersMenu()
        {
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }

            // Tìm menu Orders đã có hoặc tạo mới
            var ordersMenu = _context.TbAdminmenus.FirstOrDefault(m => m.Alias == "Orders");
            
            if (ordersMenu != null)
            {
                // Kích hoạt menu đã có
                ordersMenu.IsActive = true;
                ordersMenu.Title = "Quản lý đơn hàng";
                ordersMenu.Icon = "fas fa-shopping-cart";
                ordersMenu.Description = "Quản lý đơn hàng của khách hàng";
                if (ordersMenu.Positon == null || ordersMenu.Positon == 0)
                {
                    var maxPosition = _context.TbAdminmenus.Where(m => m.ParentId == 0).Max(m => (int?)m.Positon) ?? 0;
                    ordersMenu.Positon = maxPosition + 1;
                }
                _context.Update(ordersMenu);
            }
            else
            {
                // Tạo menu mới
                var maxMenuId = _context.TbAdminmenus.Max(m => (int?)m.MenuId) ?? 0;
                var maxPosition = _context.TbAdminmenus.Where(m => m.ParentId == 0).Max(m => (int?)m.Positon) ?? 0;
                
                ordersMenu = new TbAdminmenu
                {
                    MenuId = maxMenuId + 1,
                    Title = "Quản lý đơn hàng",
                    Alias = "Orders",
                    Icon = "fas fa-shopping-cart",
                    ParentId = 0,
                    Positon = maxPosition + 1,
                    Description = "Quản lý đơn hàng của khách hàng",
                    IsActive = true
                };
                _context.TbAdminmenus.Add(ordersMenu);
            }

            _context.SaveChanges();
            Function._Message = "Đã kích hoạt menu Quản lý đơn hàng thành công!";
            return RedirectToAction("Index");
        }

    }
}
