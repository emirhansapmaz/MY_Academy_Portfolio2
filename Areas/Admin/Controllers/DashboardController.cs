using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MY_Academy_Portfolio2.Context;
using MY_Academy_Portfolio2.Models;

namespace MY_Academy_Portfolio2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;
        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var totalMessages = _context.Messages.Count();
            var unreadMessages = _context.Messages.Count(x => !x.IsRead);
            var readMessages = _context.Messages.Count(x => x.IsRead);
            var totalProjects = _context.Projects.Count();
            var totalSkills = _context.Skills.Count();
            var totalExperiences = _context.Experiences.Count();
            var activeExperiences = _context.Experiences.Count(x => x.IsActive);
            var totalEducations = _context.Educations.Count();
            var totalCategories = _context.Categories.Count();

            // Son 5 Mesaj (tarihe göre azalan)
            var recentMessages = _context.Messages
                .OrderByDescending(x => x.SendDate)
                .Take(5)
                .ToList();

            // Son 3 Proje (kategorisi ile birlikte)
            var recentProjects = _context.Projects
                .Include(x => x.Category)
                .OrderByDescending(x => x.ProjectId)
                .Take(3)
                .ToList();

            // Kategori bazında proje dağılımı (GroupBy)
            var categoryStats = _context.Categories
                .Select(c => new
                {
                    c.CategoryName,
                    Count = c.Projects.Count()
                })
                .ToList();

            var categorySummaries = categoryStats.Select(cs => new CategorySummaryItem
            {
                CategoryName = cs.CategoryName,
                ProjectCount = cs.Count,
                Percentage = totalProjects > 0 ? (int)Math.Round(cs.Count * 100.0 / totalProjects) : 0
            }).ToList();

            var model = new DashboardViewModel
            {
                TotalProjects = totalProjects,
                ActiveProjects = totalProjects,
                TotalSkills = totalSkills,
                TotalExperiences = totalExperiences,
                ActiveExperiences = activeExperiences,
                TotalEducations = totalEducations,
                TotalMessages = totalMessages,
                UnreadMessages = unreadMessages,
                ReadMessages = readMessages,
                TotalCategories = totalCategories,
                RecentMessages = recentMessages,
                RecentProjects = recentProjects,
                CategorySummaries = categorySummaries
            };

            return View(model);
        }
    }
}
