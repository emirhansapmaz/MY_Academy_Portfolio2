using Microsoft.AspNetCore.Mvc;
using MY_Academy_Portfolio2.Context;
using MY_Academy_Portfolio2.Entities;

namespace MY_Academy_Portfolio2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;
        public MessageController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult MessageList()
        {
            var values = _context.Messages.ToList();
            return View(values);
        }

        public IActionResult MessageDetail(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                value.IsRead = true;
                _context.SaveChanges();
            }
            return View(value);
        }

        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            _context.Messages.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }
    }
}
