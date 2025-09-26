namespace EventifyPass.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }   // Primary Key
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Foreign Key
        public int EventId { get; set; }
        public Event? Event { get; set; }
    }
}
