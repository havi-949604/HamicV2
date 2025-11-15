using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Harmic.Utilities;
using Harmic.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Attributes
{
    public class AuthorizePermissionAttribute : ActionFilterAttribute
    {
        private readonly string _permissionName;
        private readonly string? _controllerName;
        private readonly string? _actionName;

        public AuthorizePermissionAttribute(string permissionName)
        {
            _permissionName = permissionName;
        }

        public AuthorizePermissionAttribute(string controllerName, string actionName)
        {
            _controllerName = controllerName;
            _actionName = actionName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Kiểm tra đăng nhập
            if (!Function.isLogin())
            {
                Function._Message = "Bạn cần phải đăng nhập";
                Function._ReturnUrl = context.HttpContext.Request.Path;
                context.Result = new RedirectResult("/Login");
                return;
            }

            // Chặn khách hàng (Role 1) vào admin
            if (Function._RoleId == 1)
            {
                Function._Message = "Bạn không có quyền truy cập trang này";
                context.Result = new RedirectResult("/Home");
                return;
            }

            // Chỉ admin (Role 2) và người bán (Role 4) mới có thể truy cập admin
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập trang này";
                context.Result = new RedirectResult("/Home");
                return;
            }

            // Lấy DbContext từ service provider
            var serviceProvider = context.HttpContext.RequestServices;
            var dbContext = serviceProvider.GetService(typeof(HarmicContext)) as HarmicContext;
            if (dbContext == null)
            {
                Function._Message = "Lỗi hệ thống";
                context.Result = new RedirectResult("/Home");
                return;
            }

            // Kiểm tra quyền cụ thể (chỉ kiểm tra nếu đã có dữ liệu quyền trong database)
            try
            {
                // Nếu có dữ liệu quyền, mới kiểm tra
                if (dbContext.TbPermissions.Any())
                {
                    bool hasPermission = false;

                    if (!string.IsNullOrEmpty(_permissionName))
                    {
                        // Kiểm tra theo tên quyền
                        hasPermission = Function.HasPermission(dbContext, _permissionName);
                    }
                    else if (!string.IsNullOrEmpty(_controllerName) && !string.IsNullOrEmpty(_actionName))
                    {
                        // Kiểm tra theo controller và action
                        hasPermission = Function.HasPermission(dbContext, _controllerName, _actionName);
                    }

                    if (!hasPermission)
                    {
                        Function._Message = "Bạn không có quyền thực hiện hành động này";
                        context.Result = new RedirectResult("/Admin/Home");
                        return;
                    }
                }
                // Nếu chưa có dữ liệu quyền, cho phép truy cập (tương thích với hệ thống cũ)
            }
            catch
            {
                // Nếu có lỗi khi kiểm tra (ví dụ bảng chưa tồn tại), cho phép truy cập (tương thích với hệ thống cũ)
            }

            base.OnActionExecuting(context);
        }
    }
}

