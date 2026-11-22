using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MY_Academy_Portfolio2.Context;
using MY_Academy_Portfolio2.Entities;

namespace MY_Academy_Portfolio2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;
        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.Categories
                .Include(x => x.Projects)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Kategori başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Include(x => x.Projects).FirstOrDefault(x => x.CategoryId == id);
            if (category != null)
            {
                // İlişkili projeler varsa güvenli silme
                if (category.Projects != null && category.Projects.Any())
                {
                    TempData["ErrorMessage"] = "Bu kategoriye bağlı projeler bulunduğu için silinemez. Önce projelerin kategorisini değiştirin veya silin.";
                    return RedirectToAction("Index");
                }

                _context.Categories.Remove(category);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Kategori başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
