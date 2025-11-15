using Microsoft.AspNetCore.Mvc;
using Harmic.Models;

namespace Harmic.Areas.Admin.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly HarmicContext _context;
        public MenuViewComponent(HarmicContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbAdminmenus
                .Where(m => m.IsActive)
                .OrderBy(m => m.Positon)
                .ToList();
            return await Task.FromResult<IViewComponentResult>(View(items));
        }
    }
}
