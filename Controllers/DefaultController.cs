using Microsoft.AspNetCore.Mvc;
using MY_Academy_Portfolio2.Context;
using MY_Academy_Portfolio2.Entities;

namespace MY_Academy_Portfolio2.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _environment;

        public DefaultController(PortfolioContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        // 04 | Bu tasarıma özel görev: CV İndir
        [HttpGet]
        public IActionResult DownloadCv()
        {
            var cvDirectory = Path.Combine(_environment.WebRootPath, "uploads", "cv");
            if (Directory.Exists(cvDirectory))
            {
                var dirInfo = new DirectoryInfo(cvDirectory);
                var latestFile = dirInfo.GetFiles("*.pdf").OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (latestFile != null && System.IO.File.Exists(latestFile.FullName))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(latestFile.FullName);
                    return File(fileBytes, "application/pdf", "Emirhan-Sapmaz-CV.pdf");
                }
            }

            // Dosya henüz yüklenmemişse bilgilendir
            TempData["ErrorMessage"] = "CV dosyası henüz yüklenmemiştir.";
            return RedirectToAction("Index");
        }

        // 03 | Mesajlar ve iletişim (Validasyon & Veritabanına kayıt)
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

                // Sunucu Tarafı Validasyon Kontrolleri (06 | Data Annotations / ModelState)
                if (string.IsNullOrWhiteSpace(message.NameSurname) || message.NameSurname.Length < 2)
                {
                    return Json(new { success = false, message = "Lütfen adınızı ve soyadınızı eksiksiz giriniz." });
                }

                if (string.IsNullOrWhiteSpace(message.Email) || !message.Email.Contains("@") || !message.Email.Contains("."))
                {
                    return Json(new { success = false, message = "Lütfen geçerli bir e-posta adresi giriniz." });
                }

                if (string.IsNullOrWhiteSpace(message.Subject))
                {
                    message.Subject = "Genel İletişim";
                }

                if (string.IsNullOrWhiteSpace(message.MessageContent) || message.MessageContent.Length < 3)
                {
                    return Json(new { success = false, message = "Lütfen mesajınızı belirtiniz." });
                }

                message.SendDate = DateTime.Now;
                message.IsRead = false;

                _context.Messages.Add(message);
                _context.SaveChanges();

                return Json(new { success = true, message = "Mesajınız başarıyla iletildi!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Mesaj iletilirken bir hata oluştu: " + ex.Message });
            }
        }
    }
}
