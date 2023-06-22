namespace ticketfinder.Models.ORM
{
    public class EventSeat
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public bool IsSold { get; set; } = false;
        public Event? Event { get; set; }
        public Seat? Seat { get; set; }

    }
}