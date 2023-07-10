using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.EventDTO
{
    public class CreateEventDTO
    {
        internal IEnumerable<string> eventImageArray;

        public string Name { get; set; } = "New Event";
        public double Price { get; set; } //
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public string? Description { get; set; }
        public EventType EventType { get; set; } //dropdown
        public string EventImages { get; set; }
        public int[]? StageIds { get; set; } //dropdown çoklu seçim //price'a göre stage'leri event stage'lere çevirecek..

   
    }
}
