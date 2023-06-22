namespace ticketfinder.Models.ORM
{
    public class EventStage
    {
        public int Id { get; set; }
        public double BasePrice { get; set; }
        public Stage? Stage { get; set; }
        public List<EventSeat>? EventSeats { get; set; }

    }
}