using Microsoft.AspNetCore.Mvc;

namespace MY_Academy_Portfolio2.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
