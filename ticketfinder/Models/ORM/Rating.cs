using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class Rating
    {
        public int Id { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}