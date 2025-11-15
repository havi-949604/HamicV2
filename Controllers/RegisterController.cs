using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public IActionResult Index(TbCustomer customer, string ConfirmPassword) { 
            if(string.IsNullOrEmpty(customer.Username) || string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                Function._Message = "Vui lòng nhập đủ các trường";
                return View();
            }
           var check = _context.TbCustomers.FirstOrDefault(i=>i.Email == customer.Email);
            if (check != null)
            {
                Function._Message = "Email đã được đăng ký";
                return View();
            }
            if(ConfirmPassword != customer.Password)
            {
                Function._Message = "Mật khẩu và mật khẩu nhập lại phải khác nhau";
                return View();
            }
            TbCustomer tbCustomer = new TbCustomer();
            tbCustomer.Avatar = "/files/Avatars/1.jpg";
            tbCustomer.Email = customer.Email;
            tbCustomer.IsActive = true;
            tbCustomer.Password = Function.md5password(customer.Password);
            tbCustomer.Username = customer.Username;
            tbCustomer.RoleId = 1;
            Function._Message = string.Empty;

            _context.Add(tbCustomer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Login");
        }
    }
}
