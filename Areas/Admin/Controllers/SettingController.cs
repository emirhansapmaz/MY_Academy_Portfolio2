using Microsoft.AspNetCore.Mvc;

namespace MY_Academy_Portfolio2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public SettingController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cvDirectory = Path.Combine(_environment.WebRootPath, "uploads", "cv");
            string currentCvName = "Henüz yüklenmemiş";
            DateTime? uploadDate = null;
            long fileSize = 0;

            if (Directory.Exists(cvDirectory))
            {
                var dirInfo = new DirectoryInfo(cvDirectory);
                var latestFile = dirInfo.GetFiles("*.pdf").OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (latestFile != null)
                {
                    currentCvName = latestFile.Name;
                    uploadDate = latestFile.LastWriteTime;
                    fileSize = latestFile.Length;
                }
            }

            ViewBag.CurrentCvName = currentCvName;
            ViewBag.UploadDate = uploadDate;
            ViewBag.FileSizeKb = fileSize > 0 ? (fileSize / 1024) : 0;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadCv(IFormFile cvFile)
        {
            if (cvFile == null || cvFile.Length == 0)
            {
                TempData["ErrorMessage"] = "Lütfen yüklenecek bir dosya seçiniz.";
                return RedirectToAction("Index");
            }

            // Yalnızca PDF kontrolü
            var extension = Path.GetExtension(cvFile.FileName).ToLowerInvariant();
            if (extension != ".pdf")
            {
                TempData["ErrorMessage"] = "Yalnızca PDF formatındaki CV dosyaları kabul edilmektedir!";
                return RedirectToAction("Index");
            }

            // Boyut kontrolü (En fazla 10 MB)
            if (cvFile.Length > 10 * 1024 * 1024)
            {
                TempData["ErrorMessage"] = "Dosya boyutu en fazla 10 MB olabilir.";
                return RedirectToAction("Index");
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "cv");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Sabit güvenli dosya adı veya benzersiz dosya adı saklama
            var filePath = Path.Combine(uploadsFolder, "emirhan-sapmaz-cv.pdf");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cvFile.CopyToAsync(stream);
            }

            TempData["SuccessMessage"] = "CV dosyası başarıyla güncellendi ve yayına alındı.";
            return RedirectToAction("Index");
        }
    }
}
