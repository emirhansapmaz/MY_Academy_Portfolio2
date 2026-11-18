using Microsoft.AspNetCore.Mvc;
using MY_Academy_Portfolio2.Context;
using MY_Academy_Portfolio2.Entities;

namespace MY_Academy_Portfolio2.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;
        public DefaultController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult SendMessage(Message message)
        {
            try
            {
                if (message == null)
                {
                    message = new Message();
                }

                if (Request.HasFormContentType)
                {
                    if (string.IsNullOrWhiteSpace(message.NameSurname))
                    {
                        message.NameSurname = Request.Form["NameSurname"].ToString();
                        if (string.IsNullOrWhiteSpace(message.NameSurname))
                        {
                            message.NameSurname = Request.Form["FullName"].ToString();
                        }
                    }

                    if (string.IsNullOrWhiteSpace(message.Email))
                    {
                        message.Email = Request.Form["Email"].ToString();
                    }

                    if (string.IsNullOrWhiteSpace(message.Subject))
                    {
                        message.Subject = Request.Form["Subject"].ToString();
                    }

                    if (string.IsNullOrWhiteSpace(message.MessageContent))
                    {
                        message.MessageContent = Request.Form["MessageContent"].ToString();
                        if (string.IsNullOrWhiteSpace(message.MessageContent))
                        {
                            message.MessageContent = Request.Form["Message"].ToString();
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(message.NameSurname))
                {
                    return Json(new { success = false, message = "Lütfen adınızı ve soyadınızı giriniz." });
                }

                message.Email ??= "";
                message.Subject ??= "Genel İletişim";
                message.MessageContent ??= "";
                message.SendDate = DateTime.Now;
                message.IsRead = false;

                _context.Messages.Add(message);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
