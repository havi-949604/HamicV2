using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;
using Harmic.Utilities;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolePermissionsController : Controller
    {
        private readonly HarmicContext _context;

        public RolePermissionsController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/RolePermissions
        public IActionResult Index()
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Login");
            }

            var rolePermissions = _context.TbRolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .OrderBy(rp => rp.Role!.RoleName)
                .ThenBy(rp => rp.Permission!.PermissionName)
                .ToList();

            return View(rolePermissions);
        }

        // GET: Admin/RolePermissions/Manage
        public IActionResult Manage()
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Login");
            }

            ViewBag.Roles = _context.TbRoles.ToList();
            ViewBag.Permissions = _context.TbPermissions.Where(p => p.IsActive).ToList();
            
            // Lấy danh sách quyền hiện có của từng role
            var rolePermissions = _context.TbRolePermissions
                .Include(rp => rp.Permission)
                .ToList()
                .GroupBy(rp => rp.RoleId)
                .ToDictionary(g => g.Key ?? 0, g => g.Select(rp => rp.PermissionId ?? 0).ToList());

            ViewBag.RolePermissions = rolePermissions;

            return View();
        }

        // POST: Admin/RolePermissions/Assign
        [HttpPost]
        public IActionResult Assign(int roleId, List<int> permissionIds)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                return Json(new { success = false, message = "Bạn không có quyền truy cập" });
            }

            try
            {
                // Xóa tất cả quyền hiện tại của role
                var existingPermissions = _context.TbRolePermissions
                    .Where(rp => rp.RoleId == roleId)
                    .ToList();
                _context.TbRolePermissions.RemoveRange(existingPermissions);

                // Thêm quyền mới
                if (permissionIds != null && permissionIds.Any())
                {
                    foreach (var permissionId in permissionIds)
                    {
                        var rolePermission = new TbRolePermission
                        {
                            RoleId = roleId,
                            PermissionId = permissionId
                        };
                        _context.TbRolePermissions.Add(rolePermission);
                    }
                }

                _context.SaveChanges();
                return Json(new { success = true, message = "Phân quyền thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // GET: Admin/RolePermissions/GetRolePermissions
        [HttpGet]
        public IActionResult GetRolePermissions(int roleId)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                return Json(new { success = false, message = "Bạn không có quyền truy cập" });
            }

            var permissionIds = _context.TbRolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId ?? 0)
                .ToList();

            return Json(new { success = true, permissionIds = permissionIds });
        }
    }
}

