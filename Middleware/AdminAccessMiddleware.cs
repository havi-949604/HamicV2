using Harmic.Utilities;
using Microsoft.AspNetCore.Http;

namespace Harmic.Middleware
{
    public class AdminAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Chỉ kiểm tra các route trong Admin area
            if (context.Request.Path.StartsWithSegments("/Admin"))
            {
                // Kiểm tra đăng nhập
                if (!Function.isLogin())
                {
                    Function._Message = "Bạn cần phải đăng nhập";
                    Function._ReturnUrl = context.Request.Path;
                    context.Response.Redirect("/Login");
                    return;
                }

                // Chặn khách hàng (Role 1) vào admin
                if (Function._RoleId == 1)
                {
                    Function._Message = "Bạn không có quyền truy cập vào trang quản trị";
                    context.Response.Redirect("/Home");
                    return;
                }
            }

            await _next(context);
        }
    }
}

