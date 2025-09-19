using System;
using System.Collections.Generic;

namespace EventifyPass.Models
{
    public class Event
    {
        //Primary Key
        public int EventId { get; set; }

        //Event Details
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //Relationships
        public int CategoryId { get; set; } // Foreign Key to Category
        public Category? Category { get; set; } // Navigation property to Category

        //Date and Time
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }

        //Location 
        public string Location { get; set; } = string.Empty;

        //Ownership
        public string Owner { get; set; } = string.Empty;

        //Metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
