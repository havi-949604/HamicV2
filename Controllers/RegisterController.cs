using System.Collections.Generic;
using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Controllers
{
    public class RegisterController : Controller
    {
        private readonly HarmicContext _context;

        public RegisterController(HarmicContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] TbCustomer customer, [FromForm] string ConfirmPassword) 
        {
            // Check if this is an AJAX request
            bool isAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            // Trim inputs
            string username = customer?.Username?.Trim() ?? string.Empty;
            string email = customer?.Email?.Trim() ?? string.Empty;
            string password = customer?.Password?.Trim() ?? string.Empty;
            string confirmPassword = ConfirmPassword?.Trim() ?? string.Empty;

            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = "Vui lòng nhập đầy đủ các trường",
                        fieldErrors = new List<string> { "Username", "Email", "Password", "ConfirmPassword" }
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

            // Validate password length
            if (password.Length < 6)
            {
                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = "Mật khẩu phải có ít nhất 6 ký tự",
                        fieldErrors = new List<string> { "Password" }
                    });
                }
                Function._Message = "Mật khẩu phải có ít nhất 6 ký tự";
                return View();
            }

            // Check if email already exists (case-insensitive)
            var check = _context.TbCustomers.FirstOrDefault(i => i.Email != null && i.Email.ToLower() == email.ToLower());
            if (check != null)
            {
                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = "Email đã được đăng ký. Vui lòng sử dụng email khác hoặc đăng nhập.",
                        fieldErrors = new List<string> { "Email" }
                    });
                }
                Function._Message = "Email đã được đăng ký";
                return View();
            }

            // Check password match (FIX: passwords must match, not be different)
            if (confirmPassword != password)
            {
                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = "Mật khẩu xác nhận không khớp. Vui lòng kiểm tra lại.",
                        fieldErrors = new List<string> { "ConfirmPassword" }
                    });
                }
                Function._Message = "Mật khẩu xác nhận không khớp";
                return View();
            }

            try
            {
                // Create new customer
                TbCustomer tbCustomer = new TbCustomer();
                tbCustomer.Avatar = "/files/Avatars/1.jpg";
                tbCustomer.Email = email;
                tbCustomer.IsActive = true;
                tbCustomer.Password = Function.md5password(password);
                tbCustomer.Username = username;
                tbCustomer.RoleId = 1;
                Function._Message = string.Empty;

                _context.Add(tbCustomer);
                _context.SaveChanges();

                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = true, 
                        message = "Đăng ký thành công! Bạn sẽ được chuyển đến trang đăng nhập.",
                        redirectUrl = "/Login"
                    });
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                // Log error (you can add logging here)
                string errorMessage = "Có lỗi xảy ra khi đăng ký. Vui lòng thử lại sau.";
                
                // Check for specific database errors
                if (ex.InnerException != null)
                {
                    var innerEx = ex.InnerException;
                    if (innerEx.Message.Contains("Duplicate entry") || innerEx.Message.Contains("UNIQUE constraint"))
                    {
                        errorMessage = "Email đã được đăng ký. Vui lòng sử dụng email khác.";
                    }
                    else if (innerEx.Message.Contains("Cannot insert NULL"))
                    {
                        errorMessage = "Thông tin không hợp lệ. Vui lòng kiểm tra lại.";
                    }
                }

                if (isAjaxRequest)
                {
                    return Json(new { 
                        success = false, 
                        message = errorMessage
                    });
                }
                
                Function._Message = errorMessage;
                return View();
            }
        }
    }
}
