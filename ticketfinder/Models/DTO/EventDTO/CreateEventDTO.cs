using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.EventDTO
{
    public class CreateEventDTO
    {
        public string Name { get; set; } = "Event";
        public double Price { get; set; } //
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;
        public EventType EventType { get; set; } //dropdown
        //public List<EventImage> EventImages { get; set; } //selection
       // public List<Stage> EventStages { get; set; } //dropdown çoklu seçim //price'a göre stage'leri event stage'lere çevirecek..

   
    }
}
