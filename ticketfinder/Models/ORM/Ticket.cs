using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class Ticket
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public int EventSeatId  { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }


    }
}
