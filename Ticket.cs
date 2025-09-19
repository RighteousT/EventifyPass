using EventifyPass.Models;

namespace EventifyPass.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public decimal Price { get; set; }
        public int Quanity { get; set; }

        // Foreign Key  
        public int EventID { get; set; }
        public Event? Event { get; set; }
    }
}


