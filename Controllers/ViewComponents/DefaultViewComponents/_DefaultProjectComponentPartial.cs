using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MY_Academy_Portfolio2.Context;

namespace MY_Academy_Portfolio2.Controllers.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Projects.Include(x => x.Category).ToList();
            return View(values);
        }
    }
}
