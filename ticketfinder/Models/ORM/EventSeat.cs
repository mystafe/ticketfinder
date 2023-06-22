namespace ticketfinder.Models.ORM
{
    public class EventSeat
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public bool IsSold { get; set; } = false;
        public int EventId  { get; set; }
        public int EventStageId { get; set; }
        public Seat? Seat { get; set; }

    }
}