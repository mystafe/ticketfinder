using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO
{
    public class CreateRatingDTO
    {
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public int EventId { get; set; }
        public int CustomerId { get; set; }
    }
}
