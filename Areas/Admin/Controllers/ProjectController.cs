using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MY_Academy_Portfolio2.Context;
using MY_Academy_Portfolio2.Entities;

namespace MY_Academy_Portfolio2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProjectController(PortfolioContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index(string? search, int? categoryId)
        {
            var query = _context.Projects.Include(x => x.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(x => x.Title.Contains(s) || x.Description.Contains(s));
                ViewBag.CurrentSearch = search;
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
                ViewBag.SelectedCategoryId = categoryId.Value;
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", categoryId);
            var values = query.OrderByDescending(x => x.ProjectId).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }

        // 06 | Dosya yönetimi: IFormFile ile proje görseli yükleme, boyut ve tip kontrolü
        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project, IFormFile? imageFile)
        {
            // Eğer görsel dosyası yüklendiyse
            if (imageFile != null && imageFile.Length > 0)
            {
                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".svg" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("CoverImageUrl", "Lütfen geçerli bir görsel formatı (.jpg, .jpeg, .png, .webp, .svg) yükleyiniz.");
                }
                else if (imageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("CoverImageUrl", "Görsel boyutu en fazla 5 MB olabilir.");
                }
                else
                {
                    var uploadsDir = Path.Combine(_environment.WebRootPath, "uploads", "projects");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    // Benzersiz dosya adı oluşturma (GUID)
                    var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine(uploadsDir, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    project.CoverImageUrl = $"/uploads/projects/{uniqueFileName}";
                }
            }

            if (string.IsNullOrWhiteSpace(project.CoverImageUrl))
            {
                project.CoverImageUrl = "https://images.unsplash.com/photo-1555066931-4365d14bab8c?w=800&auto=format&fit=crop&q=80";
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", project.CategoryId);
                return View(project);
            }

            _context.Projects.Add(project);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Proje başarıyla kaydedildi.";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value != null)
            {
                _context.Projects.Remove(value);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Proje başarıyla silindi.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", value.CategoryId);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(Project project, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".svg" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("CoverImageUrl", "Lütfen geçerli bir görsel formatı yükleyiniz.");
                }
                else if (imageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("CoverImageUrl", "Görsel boyutu en fazla 5 MB olabilir.");
                }
                else
                {
                    var uploadsDir = Path.Combine(_environment.WebRootPath, "uploads", "projects");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine(uploadsDir, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    project.CoverImageUrl = $"/uploads/projects/{uniqueFileName}";
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", project.CategoryId);
                return View(project);
            }

            _context.Projects.Update(project);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Proje başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
