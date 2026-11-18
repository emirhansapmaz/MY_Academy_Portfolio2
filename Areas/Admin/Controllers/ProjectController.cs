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
        public ProjectController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.Projects.Include(x => x.Category).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value != null)
            {
                _context.Projects.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            var value = _context.Projects.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}