using Microsoft.AspNetCore.Mvc;
using MY_Academy_Portfolio2.Context;
using MY_Academy_Portfolio2.Entities;

namespace MY_Academy_Portfolio2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;
        public AboutController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var value = _context.Abouts.FirstOrDefault();
            if (value == null)
            {
                return RedirectToAction("CreateAbout");
            }
            return View(value);
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAbout(About about)
        {
            _context.Abouts.Add(about);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateAbout(About about)
        {
            _context.Abouts.Update(about);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            if (value != null)
            {
                _context.Abouts.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}