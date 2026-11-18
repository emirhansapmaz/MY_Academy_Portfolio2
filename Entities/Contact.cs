using Microsoft.Identity.Client;

namespace MY_Academy_Portfolio2.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string LinkedIn { get; set; }
        public string Location { get; set; }
        public string Github { get; set; }
        public string Subscription { get; set; }
    }
}
