using Microsoft.AspNetCore.Mvc;
using MY_Academy_Portfolio2.Context;

namespace MY_Academy_Portfolio2.Controllers.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultAboutComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var value = _context.Abouts.FirstOrDefault();
            return View(value);
        }
    }
}