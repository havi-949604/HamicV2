using System.Collections.Generic;
using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Controllers
{
    public class LoginController : Controller
    {
        private static HarmicContext _context;

        public LoginController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (Function.isLogin())
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Bạn đã đăng nhập rồi" });
                }
                Function._Message = "Bạn đã đăng nhập rồi";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(TbCustomer customer)
        {
            // Check if this is an AJAX request
            bool isAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (customer == null)
            {
                if (isAjaxRequest)
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
                }
                return NotFound();
            }

            // Trim email and password to remove whitespace
            string email = customer.Email?.Trim() ?? string.Empty;
            string password = customer.Password?.Trim() ?? string.Empty;

            // Validate input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = "Vui lòng nhập đầy đủ email và mật khẩu",
                        fieldErrors = new List<string> { "Email", "Password" }
                    });
                }
                Function._Message = "Vui lòng nhập đủ các trường";
                return View();
            }

            // Validate email format
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = "Email không hợp lệ",
                        fieldErrors = new List<string> { "Email" }
                    });
                }
                Function._Message = "Email không hợp lệ";
                return View();
            }

            // Check customer - compare email case-insensitive
            string hashedPassword = Function.md5password(password);
            
            // First, try to find by email (case-insensitive)
            var customerByEmail = _context.TbCustomers
                .Where(i => i.Email != null && i.Email.ToLower() == email.ToLower())
                .FirstOrDefault();
            
            if (customerByEmail == null)
            {
                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = "Email không tồn tại trong hệ thống. Vui lòng kiểm tra lại.",
                        fieldErrors = new List<string> { "Email" }
                    });
                }
                Function._Message = "Email không tồn tại";
                return View();
            }
            
            // Check password
            if (customerByEmail.Password != hashedPassword)
            {
                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = "Mật khẩu không đúng. Vui lòng kiểm tra lại.",
                        fieldErrors = new List<string> { "Password" }
                    });
                }
                Function._Message = "Sai mật khẩu";
                return View();
            }
            
            var check = customerByEmail;

            // Check if account is active
            if (!check.IsActive)
            {
                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ quản trị viên."
                    });
                }
                Function._Message = "Tài khoản của bạn đã bị khóa";
                return View();
            }

            // Update last login
            check.LastLogin = DateTime.Now;
            _context.Update(check);
            _context.SaveChanges();

            // Set session/login info
            Function._AccountId = check.CustomerId;
            Function._FullName = check.Username;
            Function._Message = string.Empty;
            Function._Email = check.Email;
            Function._RoleId = check.RoleId;

            // Determine redirect URL
            string redirectUrl = "/Home";
            if (!string.IsNullOrEmpty(Function._ReturnUrl))
            {
                redirectUrl = Function._ReturnUrl;
                Function._ReturnUrl = string.Empty;
            }

            if (isAjaxRequest)
            {
                return Json(new { 
                    success = true, 
                    message = "Đăng nhập thành công!",
                    redirectUrl = redirectUrl
                });
            }

            return Redirect(redirectUrl);
        }
    }
}
