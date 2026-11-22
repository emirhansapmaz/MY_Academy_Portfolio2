using Microsoft.AspNetCore.Mvc;
using MY_Academy_Portfolio2.Context;

namespace MY_Academy_Portfolio2.Controllers.ViewComponents.DefaultViewComponents
{
    public class _DefaultExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            // Case 01: Sadece aktif deneyimler listelenir (pasif olanlar UI tarafında görünmez), tarihe göre azalan
            var values = _context.Experiences
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.ExperienceId)
                .ToList();
            return View(values);
        }
    }
}
