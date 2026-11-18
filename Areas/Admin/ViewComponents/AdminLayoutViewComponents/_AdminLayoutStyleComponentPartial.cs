using Microsoft.AspNetCore.Mvc;

namespace MY_Academy_Portfolio2.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutStyleComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
