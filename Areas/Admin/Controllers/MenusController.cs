using Harmic.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenusController : Controller
    {
        private readonly HarmicContext _context;

        public MenusController(HarmicContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> MoveUp(int id)
        {
            var menu = await _context.TbMenus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            if (menu.Position > 1)
            {
                var previousMenu = await _context.TbMenus
                    .Where(i => i.Position == menu.Position - 1)
                    .FirstOrDefaultAsync();
                menu.Position -= 1;
                if (previousMenu != null)
                {
                    previousMenu.Position += 1;
                    _context.Update(previousMenu);
                }
                _context.TbMenus.Update(menu);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MoveDown(int id)
        {
            var menu = await _context.TbMenus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            if (menu.Position < _context.TbMenus.Max(i => i.Position))
            {
                var nextmenu = await _context.TbMenus
                    .Where(i => i.Position == menu.Position + 1)
                    .FirstOrDefaultAsync();
                menu.Position += 1;
                if (nextmenu != null)
                {
                    nextmenu.Position -= 1;
                    _context.Update(nextmenu);
                }
                _context.TbMenus.Update(menu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangeShow(int id)
        {
            var menu = await _context.TbMenus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            menu.IsActive = !menu.IsActive;
            _context.TbMenus.Update(menu);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: Admin/Menus
        public async Task<IActionResult> Index()
        {
            if (Function.isLogin())
            {
                return View(await _context.TbMenus.OrderBy(i=>i.Position).ToListAsync());
            }
            return Redirect("/Login");
        }

        // GET: Admin/Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (Function.isLogin())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbMenu = await _context.TbMenus
                    .FirstOrDefaultAsync(m => m.MenuId == id);

                ViewBag.Menus = _context.TbMenus.ToList();

                if (tbMenu == null)
                {
                    return NotFound();
                }

                return View(tbMenu);
            }
            return RedirectToAction("Index", "Login");

        }

        // GET: Admin/Menus/Create
        public IActionResult Create()
        {
            if (Function.isLogin())
            {
                ViewBag.Menus = new SelectList(_context.TbMenus.Where(i => i.Levels == 1).ToList(), "MenuId", "Title");
                return View();
            }
            return RedirectToAction("Index", "Login");
                    }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,Title,Alias,Description,Levels,ParentId,Position,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] TbMenu tbMenu)
        {
            if (Function.isLogin())
            {

                tbMenu.CreatedDate = DateTime.Now;
                tbMenu.CreatedBy = Function._FullName;
                tbMenu.Position = _context.TbMenus.Any() ? _context.TbMenus.Max(x => x.Position) + 1 : 1;
                if (tbMenu.ParentId == 0)
                    tbMenu.Levels = 1;
                else
                    tbMenu.Levels = 2;

                if (ModelState.IsValid)
                {
                    _context.Add(tbMenu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(tbMenu);
            }
            return Redirect("/Login");

        }

        // GET: Admin/Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (Function.isLogin())
            {
                if (id == null)
                {
                    return NotFound();
                }

                ViewData["Menus"] = new SelectList(_context.TbMenus.Where(i=>i.Levels == 1), "MenuId", "Title");

                var tbMenu = await _context.TbMenus.FindAsync(id);
                if (tbMenu == null)
                {
                    return NotFound();
                }
                return View(tbMenu);
            }
            return RedirectToAction("Index", "Login");

        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuId,Title,Alias,Description,Levels,ParentId,IsActive")] TbMenu tbMenu)
        {
            if (Function.isLogin())
            {
                if (id != tbMenu.MenuId)
                {
                    return NotFound();
                }

                tbMenu.ModifiedDate = DateTime.Now;
                tbMenu.ModifiedBy = Function._FullName;


                if (tbMenu.ParentId == 0)
                    tbMenu.Levels = 1;
                else
                    tbMenu.Levels = 2;

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(tbMenu);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TbMenuExists(tbMenu.MenuId))
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
                return View(tbMenu);
            }
            return RedirectToAction("Index", "Login");

        }

        // GET: Admin/Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (Function.isLogin())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbMenu = await _context.TbMenus
                    .FirstOrDefaultAsync(m => m.MenuId == id);
                if (tbMenu == null)
                {
                    return NotFound();
                }

                return View(tbMenu);
            }
            return RedirectToAction("Index", "Login");

        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Function.isLogin())
            {
                var tbMenu = await _context.TbMenus.FindAsync(id);
                if (tbMenu != null)
                {
                    _context.TbMenus.Remove(tbMenu);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Login");

        }

        private bool TbMenuExists(int id)
        {
            return _context.TbMenus.Any(e => e.MenuId == id);
        }
    }
}
