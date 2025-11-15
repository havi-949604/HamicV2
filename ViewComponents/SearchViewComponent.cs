using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
namespace Harmic.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly HarmicContext _context;

        public SearchViewComponent(HarmicContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _context.TbProductcategories.Where(i=>i.IsActive).OrderBy(i => i.Position).ToList();
            ViewBag.Categories = categories;
            return await Task.FromResult<IViewComponentResult>(View(new Harmic.ViewModels.ProductSearchViewModel()));
        }
    }
}
