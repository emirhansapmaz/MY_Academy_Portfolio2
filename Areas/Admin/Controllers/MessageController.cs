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

        public IActionResult Index(string search, string status, string sort)
        {
            return RedirectToAction("MessageList", new { search, status, sort });
        }

        // 03 | Arama, Okundu/Okunmadı filtresi, Tarihe göre sıralama ve Okunmamış mesaj sayısı
        public IActionResult MessageList(string? search, string? status, string? sort)
        {
            var query = _context.Messages.AsQueryable();

            // Okunmamış ve toplam istatistikler
            ViewBag.TotalCount = _context.Messages.Count();
            ViewBag.UnreadCount = _context.Messages.Count(x => !x.IsRead);
            ViewBag.ReadCount = _context.Messages.Count(x => x.IsRead);

            // Arama filtresi (Gönderen veya Konu)
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(x => x.NameSurname.Contains(s) || x.Subject.Contains(s) || x.Email.Contains(s));
                ViewBag.CurrentSearch = search;
            }

            // Durum filtresi (read / unread)
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (status == "unread")
                {
                    query = query.Where(x => !x.IsRead);
                }
                else if (status == "read")
                {
                    query = query.Where(x => x.IsRead);
                }
                ViewBag.CurrentStatus = status;
            }

            // Sıralama (tarihe göre azalan / artan)
            if (sort == "oldest")
            {
                query = query.OrderBy(x => x.SendDate);
                ViewBag.CurrentSort = "oldest";
            }
            else
            {
                query = query.OrderByDescending(x => x.SendDate);
                ViewBag.CurrentSort = "newest";
            }

            var values = query.ToList();
            return View(values);
        }

        public IActionResult MessageDetail(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                if (!value.IsRead)
                {
                    value.IsRead = true;
                    _context.SaveChanges();
                }
            }
            return View(value);
        }

        public IActionResult GetMessageDetail(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                if (!value.IsRead)
                {
                    value.IsRead = true;
                    _context.SaveChanges();
                }
                return Json(new
                {
                    success = true,
                    id = value.MessageId,
                    name = value.NameSurname,
                    email = value.Email,
                    subject = value.Subject,
                    date = value.SendDate.ToString("dd MMMM yyyy HH:mm"),
                    content = value.MessageContent
                });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null && !value.IsRead)
            {
                value.IsRead = true;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return View(message);
            }

            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Mesaj başarıyla oluşturuldu.";
            return RedirectToAction("MessageList");
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                _context.Messages.Remove(value);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Mesaj başarıyla silindi.";
            }
            return RedirectToAction("MessageList");
        }
    }
}
