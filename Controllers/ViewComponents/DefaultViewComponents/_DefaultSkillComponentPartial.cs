using Microsoft.AspNetCore.Mvc;
using MY_Academy_Portfolio2.Context;

namespace MY_Academy_Portfolio2.Controllers.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultSkillComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Skills.ToList();
            return View(values);
        }
    }
}
