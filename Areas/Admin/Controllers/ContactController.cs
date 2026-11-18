using Microsoft.AspNetCore.Mvc;

namespace MY_Academy_Portfolio2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("MessageList", "Message");
        }
    }
}
