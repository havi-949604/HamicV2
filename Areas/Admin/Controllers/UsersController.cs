using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;
using Harmic.Utilities;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly HarmicContext _context;

        public UsersController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public IActionResult Index(string search = "", int? roleId = null)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Login");
            }

            var query = _context.TbCustomers
                .Include(u => u.Role)
                .AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => 
                    (u.Username != null && u.Username.Contains(search)) ||
                    (u.Email != null && u.Email.Contains(search)) ||
                    (u.Phone != null && u.Phone.Contains(search))
                );
            }

            // Lọc theo role
            if (roleId.HasValue && roleId.Value > 0)
            {
                query = query.Where(u => u.RoleId == roleId.Value);
            }

            var users = query.OrderByDescending(u => u.CustomerId).ToList();
            ViewBag.Roles = _context.TbRoles.OrderBy(r => r.RoleId).ToList();
            ViewBag.Search = search;
            ViewBag.SelectedRoleId = roleId;

            return View(users);
        }

        // POST: Admin/Users/ChangeRole
        [HttpPost]
        public IActionResult ChangeRole(int userId, int roleId)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                return Json(new { success = false, message = "Bạn không có quyền truy cập" });
            }

            try
            {
                var user = _context.TbCustomers.Find(userId);
                if (user == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy user" });
                }

                // Không cho phép đổi role của chính mình
                if (user.CustomerId == Function._AccountId)
                {
                    return Json(new { success = false, message = "Bạn không thể đổi role của chính mình" });
                }

                user.RoleId = roleId;
                _context.Update(user);
                _context.SaveChanges();

                var role = _context.TbRoles.Find(roleId);
                return Json(new { success = true, message = $"Đã cập nhật role thành công: {role?.RoleName}" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // POST: Admin/Users/ToggleActive
        [HttpPost]
        public IActionResult ToggleActive(int userId)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                return Json(new { success = false, message = "Bạn không có quyền truy cập" });
            }

            try
            {
                var user = _context.TbCustomers.Find(userId);
                if (user == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy user" });
                }

                // Không cho phép vô hiệu hóa chính mình
                if (user.CustomerId == Function._AccountId)
                {
                    return Json(new { success = false, message = "Bạn không thể vô hiệu hóa chính mình" });
                }

                user.IsActive = !user.IsActive;
                _context.Update(user);
                _context.SaveChanges();

                return Json(new { 
                    success = true, 
                    message = user.IsActive ? "Đã kích hoạt tài khoản" : "Đã vô hiệu hóa tài khoản",
                    isActive = user.IsActive
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }
    }
}

