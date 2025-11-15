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
                Function._Message = "Bạn đã đăng nhập rồi";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(TbCustomer customer)
        {
            if (customer == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Password))
            {
                Function._Message = "Vui lòng nhập đủ các trường";
                return View();
            }
            string password = Function.md5password(customer.Password);
            var check = _context.TbCustomers.Where(i => (i.Email == customer.Email) && (i.Password == password)).FirstOrDefault();
            if (check == null)
            {
                Function._Message = "Sai thông tin đăng nhập";
                return View();
            }
            check.LastLogin = DateTime.Now;
            _context.Update(check);
            _context.SaveChanges();

            Function._AccountId = check.CustomerId;
            Function._FullName = check.Username;
            Function._Message = string.Empty;
            Function._Email = check.Email;
            Function._RoleId = check.RoleId;

            if (string.IsNullOrEmpty(Function._ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string url = Function._ReturnUrl;
                Function._ReturnUrl = string.Empty;

                return Redirect(url);
            }

        }


    }
}
