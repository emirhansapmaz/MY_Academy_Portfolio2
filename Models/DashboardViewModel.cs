using MY_Academy_Portfolio2.Entities;

namespace MY_Academy_Portfolio2.Models
{
    public class DashboardViewModel
    {
        // 05 | Dashboard İstatistik Kartları
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int TotalSkills { get; set; }
        public int TotalExperiences { get; set; }
        public int ActiveExperiences { get; set; }
        public int TotalEducations { get; set; }
        public int TotalMessages { get; set; }
        public int UnreadMessages { get; set; }
        public int ReadMessages { get; set; }
        public int TotalCategories { get; set; }

        // Son 5 Mesaj
        public List<Message> RecentMessages { get; set; } = new();

        // Son 3 Proje
        public List<Project> RecentProjects { get; set; } = new();

        // Kategori Bazında Proje Sayıları Özeti
        public List<CategorySummaryItem> CategorySummaries { get; set; } = new();
    }

    public class CategorySummaryItem
    {
        public string CategoryName { get; set; } = string.Empty;
        public int ProjectCount { get; set; }
        public int Percentage { get; set; }
    }
}
