using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;
using Harmic.Utilities;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionsController : Controller
    {
        private readonly HarmicContext _context;

        public PermissionsController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/Permissions
        public IActionResult Index()
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                // Nếu là AJAX request, trả về JSON
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Bạn không có quyền truy cập" });
                }
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }

            // Loại bỏ trùng lặp: chỉ hiển thị permission đầu tiên theo ControllerName và ActionName
            var permissions = _context.TbPermissions
                .OrderBy(p => p.ControllerName)
                .ThenBy(p => p.ActionName)
                .ThenBy(p => p.PermissionId)
                .GroupBy(p => new { p.ControllerName, p.ActionName })
                .Select(g => g.First())
                .ToList();
            return View(permissions);
        }

        // GET: Admin/Permissions/Create
        public IActionResult Create()
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Login");
            }
            return View();
        }

        // POST: Admin/Permissions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TbPermission permission)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Login");
            }

            if (ModelState.IsValid)
            {
                permission.IsActive = true;
                _context.TbPermissions.Add(permission);
                _context.SaveChanges();
                Function._Message = "Thêm quyền thành công";
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }

        // GET: Admin/Permissions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var permission = _context.TbPermissions.Find(id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // POST: Admin/Permissions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TbPermission permission)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Login");
            }

            if (id != permission.PermissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permission);
                    _context.SaveChanges();
                    Function._Message = "Cập nhật quyền thành công";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionExists(permission.PermissionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }

        // GET: Admin/Permissions/Delete/5
        public IActionResult Delete(int? id)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var permission = _context.TbPermissions
                .FirstOrDefault(m => m.PermissionId == id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // POST: Admin/Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!Function.isLogin() || !Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Login");
            }

            var permission = _context.TbPermissions.Find(id);
            if (permission != null)
            {
                _context.TbPermissions.Remove(permission);
                _context.SaveChanges();
                Function._Message = "Xóa quyền thành công";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PermissionExists(int id)
        {
            return _context.TbPermissions.Any(e => e.PermissionId == id);
        }
    }
}

