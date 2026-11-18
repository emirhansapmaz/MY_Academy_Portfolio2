namespace MY_Academy_Portfolio2.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Project> Projects { get; set; }
    }
}
