using Microsoft.AspNetCore.Mvc;
namespace MY_Academy_Portfolio2.Controllers.ViewComponents.DefaultViewComponents
{
    public class _DefaultNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        { 
            return View(); 
        
        }
    }
}
