using System.Collections.Generic;

namespace EventifyPass.Models
{
    public class Category
    {
        public int CategoryId { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public List<Event>? Events { get; set; }
    }
}
