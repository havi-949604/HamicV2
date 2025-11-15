using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Harmic.ViewComponents
{
    public class MiniCartViewComponent : ViewComponent
    {
        private readonly HarmicContext _context;

        public MiniCartViewComponent(HarmicContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
             var minicart =  _context.TbCarts.Include(i=>i.IdProductNavigation).Where(i=>i.IdCustomer == Function._AccountId).ToList();

            return await Task.FromResult<IViewComponentResult>(View(minicart));
        }
    }
}
