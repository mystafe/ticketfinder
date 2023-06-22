namespace ticketfinder.Models.ORM
{
    public class EventStage
    {
        public int Id { get; set; }
        public double BasePrice { get; set; }
        public int EventId { get; set; }
        public Stage? Stage { get; set; }

        public List<EventSeat>? EventSeats { get; set; }

        public void SetSeats(List<Seat> seats)
        {
            EventSeats =seats.Select(s =>         
                    new EventSeat()
                    {
                        IsSold = false,
                        Price = s.Type == SeatType.Normal ? BasePrice : BasePrice * 1.5,
                        Seat = s,
                        EventId = EventId,
                        EventStageId=Id
                   }
                ).ToList(); 
       }

    }
}