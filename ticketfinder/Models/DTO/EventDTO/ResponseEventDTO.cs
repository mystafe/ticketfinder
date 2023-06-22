
using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.EventDTO
{
    public class ResponseEventDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsOnSale { get; set; } = true;
        public bool IsAvailable { get; set; } = true;
        public double AvgRating { get; set; }
        public EventType EventType { get; set; }
        public List<EventImage>? EventImages { get; set; }
        public List<EventStage>? EventStages { get; set; }

    }
}
