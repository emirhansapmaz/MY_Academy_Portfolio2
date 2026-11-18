using Microsoft.AspNetCore.Mvc;
using MY_Academy_Portfolio2.Context;

namespace MY_Academy_Portfolio2.Controllers.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.HeroSections.FirstOrDefault();
            ViewBag.v1 = values.Title;
            ViewBag.v2 = values.Subtitle;
            ViewBag.v3 = values.Description;
            ViewBag.v4 = values.ImageUrl;
            return View();
        }
    }
}
