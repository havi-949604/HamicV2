using Harmic.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly HarmicContext _context;

        public ProductsController(HarmicContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ChangeShow(int id)
        {
            var product = await _context.TbProducts.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.IsActive = !product.IsActive;
            _context.TbProducts.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            if (Function.isLogin())
            {
                var harmicContext = _context.TbProducts.Include(t => t.CategoryProduct);
                return View(await harmicContext.ToListAsync());
            }
            return RedirectToAction("Index", "Login");

        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            if (Function.isLogin())
            {
                ViewData["CategoryProductId"] = new SelectList(_context.TbProductcategories, "CategoryProductId", "Title");
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Title,Alias,CategoryProductId,Description,Detail,Image,Price,PriceSale,Quantity,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsNew,IsBestSeller,UnitInStock,IsActive,Star")] TbProduct tbProduct)
        {
            if (Function.isLogin())
            {
                tbProduct.Alias = Function.TitleSlugGenerationAlias(tbProduct.Title);
                tbProduct.CreatedDate = DateTime.Now;
                tbProduct.CreatedBy = Function._FullName;
                if (ModelState.IsValid)
                {
                    _context.Add(tbProduct);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoryProductId"] = new SelectList(_context.TbProductcategories, "CategoryProductId", "Title", tbProduct.CategoryProductId);
                return View(tbProduct);
            }
            return RedirectToAction("Index", "Login");

        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (Function.isLogin())
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbProduct = await _context.TbProducts.FindAsync(id);
                if (tbProduct == null)
                {
                    return NotFound();
                }
                ViewData["CategoryProductId"] = new SelectList(_context.TbProductcategories, "CategoryProductId", "Title", tbProduct.CategoryProductId);
                return View(tbProduct);
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title,Alias,CategoryProductId,Description,Detail,Image,Price,PriceSale,Quantity,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsNew,IsBestSeller,UnitInStock,IsActive,Star")] TbProduct tbProduct)
        {
            if (Function.isLogin())
            {
                if (id != tbProduct.ProductId)
                {
                    return NotFound();
                }

                tbProduct.ModifiedBy = Function._FullName;
                tbProduct.ModifiedDate = DateTime.Now;

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(tbProduct);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TbProductExists(tbProduct.ProductId))
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
                ViewData["CategoryProductId"] = new SelectList(_context.TbProductcategories, "CategoryProductId", "CategoryProductId", tbProduct.CategoryProductId);
                return View(tbProduct);
            }
            return RedirectToAction("Index", "Login");

        }

        private bool TbProductExists(int id)
        {
            return _context.TbProducts.Any(e => e.ProductId == id);
        }
    }
}
