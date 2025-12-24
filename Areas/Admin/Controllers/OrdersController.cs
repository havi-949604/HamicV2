using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Harmic.Models;
using Harmic.Utilities;
using Harmic.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Harmic.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthorizePermission("Orders", "Index")]
    public class OrdersController : Controller
    {
        private readonly HarmicContext _context;

        public OrdersController(HarmicContext context)
        {
            _context = context;
        }

        // GET: Admin/Orders
        public IActionResult Index(string search = "", int? statusId = null)
        {
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }

            var query = _context.TbOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderStatus)
                .Include(o => o.TbOrderdetails)
                .AsQueryable();

            // Tìm kiếm theo mã đơn hàng, tên khách hàng, số điện thoại
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o => 
                    (o.Code != null && o.Code.Contains(search)) ||
                    (o.CustomerName != null && o.CustomerName.Contains(search)) ||
                    (o.Phone != null && o.Phone.Contains(search))
                );
            }

            // Lọc theo trạng thái
            if (statusId.HasValue && statusId.Value > 0)
            {
                query = query.Where(o => o.OrderStatusId == statusId.Value);
            }

            var orders = query.OrderByDescending(o => o.CreatedDate).ToList();
            
            ViewBag.OrderStatuses = _context.TbOrderstatuses.ToList();
            ViewBag.Search = search;
            ViewBag.SelectedStatusId = statusId;

            return View(orders);
        }

        // GET: Admin/Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var order = _context.TbOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderStatus)
                .Include(o => o.TbOrderdetails)
                .FirstOrDefault(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            // Load thông tin sản phẩm
            var productIds = order.TbOrderdetails.Where(od => od.ProductId.HasValue).Select(od => od.ProductId.Value).ToList();
            var products = _context.TbProducts.Where(p => productIds.Contains(p.ProductId)).ToList();
            ViewBag.Products = products.ToDictionary(p => p.ProductId, p => p);

            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var order = _context.TbOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderStatus)
                .Include(o => o.TbOrderdetails)
                .FirstOrDefault(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            ViewData["OrderStatusId"] = new SelectList(_context.TbOrderstatuses, "OrderStatusId", "Name", order.OrderStatusId);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TbOrder order)
        {
            if (!Function.canAccessAdmin())
            {
                Function._Message = "Bạn không có quyền truy cập";
                return Redirect("/Admin/Home");
            }

            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingOrder = _context.TbOrders.Find(id);
                    if (existingOrder == null)
                    {
                        return NotFound();
                    }

                    // Chỉ cập nhật trạng thái đơn hàng
                    existingOrder.OrderStatusId = order.OrderStatusId;
                    
                    _context.Update(existingOrder);
                    _context.SaveChanges();
                    
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["OrderStatusId"] = new SelectList(_context.TbOrderstatuses, "OrderStatusId", "Name", order.OrderStatusId);
            return View(order);
        }

        private bool OrderExists(int id)
        {
            return _context.TbOrders.Any(e => e.OrderId == id);
        }
    }
}

