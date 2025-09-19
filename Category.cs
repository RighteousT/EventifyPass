using EventifyPass.Models;

namespace EventifyPass.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property  
        public List<Event>? Events { get; set; }

    }
}


