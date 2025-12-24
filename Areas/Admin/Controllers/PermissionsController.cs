using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;
using Harmic.Utilities;
using System.Reflection;

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

        // Lấy danh sách tất cả controllers và actions
        private Dictionary<string, List<string>> GetAllControllersAndActions()
        {
            var controllers = new Dictionary<string, List<string>>();
            
            try
            {
                // Lấy tất cả các assembly
                var assembly = Assembly.GetExecutingAssembly();
                
                // Lấy tất cả các controller trong Areas/Admin
                var adminControllers = assembly.GetTypes()
                    .Where(t => t.IsClass 
                        && t.Namespace != null 
                        && t.Namespace.Contains("Harmic.Areas.Admin.Controllers")
                        && t.Name.EndsWith("Controller")
                        && typeof(Controller).IsAssignableFrom(t))
                    .ToList();

                foreach (var controller in adminControllers)
                {
                    var controllerName = controller.Name.Replace("Controller", "");
                    var actions = new List<string>();

                    // Lấy tất cả các public methods có return type là IActionResult hoặc Task<IActionResult>
                    var methods = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Where(m => 
                            (typeof(IActionResult).IsAssignableFrom(m.ReturnType) || 
                             (m.ReturnType.IsGenericType && 
                              m.ReturnType.GetGenericTypeDefinition() == typeof(Task<>) &&
                              m.ReturnType.GetGenericArguments().Length > 0 &&
                              typeof(IActionResult).IsAssignableFrom(m.ReturnType.GetGenericArguments()[0]))) &&
                            !m.IsSpecialName &&
                            !m.GetCustomAttributes(typeof(NonActionAttribute), false).Any() &&
                            !m.IsAbstract)
                        .Select(m => m.Name)
                        .Distinct()
                        .OrderBy(a => a)
                        .ToList();

                    if (methods.Any())
                    {
                        controllers[controllerName] = methods;
                    }
                }
            }
            catch (Exception ex)
            {
                // Nếu reflection thất bại, sử dụng danh sách hardcode
                return GetHardcodedControllersAndActions();
            }

            // Nếu không tìm thấy controllers bằng reflection, sử dụng danh sách hardcode
            if (!controllers.Any())
            {
                return GetHardcodedControllersAndActions();
            }

            return controllers;
        }

        // Danh sách controllers và actions hardcode làm fallback
        private Dictionary<string, List<string>> GetHardcodedControllersAndActions()
        {
            return new Dictionary<string, List<string>>
            {
                { "Home", new List<string> { "Index" } },
                { "Products", new List<string> { "Index", "Create", "Edit", "Details", "Delete", "ChangeShow" } },
                { "ProductCategories", new List<string> { "Index", "Create", "Edit", "Details", "Delete" } },
                { "ProductReviews", new List<string> { "Index" } },
                { "Blogs", new List<string> { "Index", "Create", "Edit" } },
                { "Categories", new List<string> { "Index", "Create", "Edit", "MoveUp", "MoveDown" } },
                { "Orders", new List<string> { "Index", "Details", "Edit" } },
                { "Menus", new List<string> { "Index", "Create", "Edit", "Details", "Delete", "MoveUp", "MoveDown", "ChangeShow" } },
                { "HomeSlider", new List<string> { "Index", "Create", "Edit", "Details", "Delete" } },
                { "FileManager", new List<string> { "Index" } },
                { "Permissions", new List<string> { "Index", "Create", "Edit", "Delete", "GetActions" } },
                { "RolePermissions", new List<string> { "Index", "Manage" } },
                { "Users", new List<string> { "Index", "ChangeRole", "ToggleActive" } }
            };
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

            try
            {
                // Lấy danh sách controllers và actions
                var controllersAndActions = GetAllControllersAndActions();
                if (controllersAndActions != null && controllersAndActions.Any())
                {
                    ViewBag.Controllers = controllersAndActions.Keys.OrderBy(c => c).ToList();
                    ViewBag.ControllersAndActions = controllersAndActions;
                }
                else
                {
                    ViewBag.Controllers = new List<string>();
                    ViewBag.ControllersAndActions = new Dictionary<string, List<string>>();
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi khi quét controllers, vẫn cho phép tạo quyền thủ công
                ViewBag.Controllers = new List<string>();
                ViewBag.ControllersAndActions = new Dictionary<string, List<string>>();
                Function._Message = "Không thể quét controllers tự động. Bạn có thể nhập thủ công.";
            }

            return View();
        }

        // API endpoint để lấy actions của một controller
        [HttpGet]
        public IActionResult GetActions(string controllerName)
        {
            var controllersAndActions = GetAllControllersAndActions();
            if (controllersAndActions.ContainsKey(controllerName))
            {
                return Json(controllersAndActions[controllerName]);
            }
            return Json(new List<string>());
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

