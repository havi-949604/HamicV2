
using Microsoft.AspNetCore.Mvc;
using Harmic.Utilities;
using Harmic.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeSliderController : Controller
    {
        private readonly HarmicContext _context;
        public HomeSliderController(HarmicContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            if (!Function.isLogin())
            {
                Function._Message = "Bạn chưa đăng nhập";
                Function._ReturnUrl = Request.Path;
                return Redirect("/Login");
            }
            if (!Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Home");
            }

            var slider = _context.TbHomesliders.OrderBy(i => i.Position).ToList();

            return View(slider);
        }

        public IActionResult Create()
        {
            if (!Function.isLogin())
            {
                Function._Message = "Bạn chưa đăng nhập";
                Function._ReturnUrl = Request.Path;
                return Redirect("/Login");
            }
            if (!Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(TbHomeslider homeslider)
        {
            homeslider.Position = _context.TbHomesliders.Count() + 1;
            _context.TbHomesliders.Add(homeslider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var item = _context.TbHomesliders.Find(id);
            if(item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(TbHomeslider homeslider)
        {
            _context.TbHomesliders.Update(homeslider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult MoveUp(int id)
        {
            if (!Function.isLogin())
            {
                Function._Message = "Bạn chưa đăng nhập!";
                Function._ReturnUrl = "/Admin/HomeSlider";
                return Redirect("/Login");
            }
            if (!Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập trang này!";
                return Redirect("/Home");
            }
            var slider = _context.TbHomesliders.Find(id);
            if (slider == null)
            {
                return NotFound();
            }
            if (slider.Position > 1)
            {
                var previousslider = _context.TbHomesliders.FirstOrDefault(c => c.Position == slider.Position - 1);
                previousslider.Position++;
                slider.Position--;
                _context.TbHomesliders.Update(previousslider);
                _context.TbHomesliders.Update(slider);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        public IActionResult MoveDown(int id)
        {

            if (!Function.isLogin())
            {
                Function._Message = "Bạn chưa đăng nhập!";
                Function._ReturnUrl = "/Admin/HomeSlider";
                return Redirect("/Login");
            }
            if (!Function.isAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập trang này!";
                return Redirect("/Home");
            }
            var slider = _context.TbHomesliders.Find(id);
            if (slider == null)
            {
                return NotFound();
            }
            var nextslider = _context.TbHomesliders.FirstOrDefault(c => c.Position == slider.Position + 1);
            if (nextslider != null)
            {
                nextslider.Position--;
                slider.Position++;
                _context.TbHomesliders.Update(nextslider);
                _context.TbHomesliders.Update(slider);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> ChangeShow(int id)
        {
            var slider = await _context.TbHomesliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            slider.IsActive = !slider.IsActive;
            _context.TbHomesliders.Update(slider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }

}