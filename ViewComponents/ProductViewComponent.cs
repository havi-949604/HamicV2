using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Harmic.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly HarmicContext _context;

        public ProductViewComponent(HarmicContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbProducts.Include(m => m.CategoryProduct).Where(m => (bool)m.IsActive && m.CategoryProduct.IsActive).Where(m => m.IsNew).Include(i=>i.TbWishlishes);
            return await Task.FromResult<IViewComponentResult>(View(items.OrderByDescending(m => m.ProductId).ToList()));
        }
    }
}
