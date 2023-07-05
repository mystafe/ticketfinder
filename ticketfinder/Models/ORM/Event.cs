using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ticketfinder.Models.ORM
{

    public enum EventType
    {
        None = 0,
        Concert ,
        Sport,
        Art,
        Cinema,
        Other=99
    }
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; } = "Event";
        public double Price { get; set; }
        public DateTime? Date { get; set; }= DateTime.Now;
        public TimeSpan? Duration { get; set; }=TimeSpan.Zero;
        public bool IsActive { get; set; } = true;
        public bool IsOnSale { get; set; }=true;
        public bool IsAvailable { get; set; } = true;

        [MinLength(0)]
        [MaxLength(5)]
        public double AvgRating { get; set; }
        public EventType EventType { get; set; }=EventType.None;
        public List<EventImage> EventImages { get; set; }
        public List<EventStage>? EventStages { get; set; }


        public void SetEventStage(List<Stage> stages)
        {

            //I am dying.. :(

     
            EventStages = stages.Select(s => new EventStage()
            {
                BasePrice = Price,
                EventId = Id,
                Stage = s,
                Name= this.Name+" at "+ s.Name,

                

          
                 EventSeats = s.Seats.Select(seat => new EventSeat()
                {
                    EventId = Id,
                    IsSold = false,
                    EventPrice = seat.Type == SeatType.Normal ? Price : Price * 1.5,
                    Seat = seat
                    

                }).ToList()
                

            }).ToList();


        }

       
    }
}
