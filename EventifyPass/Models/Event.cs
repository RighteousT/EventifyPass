using System;

namespace EventifyPass.Models
{
    public class Event
    {
        public int EventId { get; set; }  // Primary Key
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public string Location { get; set; } = string.Empty;

        
        public string? Owner { get; set; }

       
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        

        // Foreign Key to Category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    
        // Event Image upload 
        public string? ImagePath { get; set; }

    }
}
