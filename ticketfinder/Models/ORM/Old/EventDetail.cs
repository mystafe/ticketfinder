//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ticketfinder.Models.ORM
//{
//    public class EventDetail
//    {
//        public int Id { get; set; }
//        public string Name { get; set; } = "Event";
//        public DateTime Date { get; set; }= DateTime.Now;
//        public TimeSpan Duration { get; set; }=TimeSpan.Zero;
//        public bool IsActive { get; set; }
//        public bool IsOnSale { get; set; }
//        public bool IsAvailable { get; set; }

//        [MinLength(0)]
//        [MaxLength(5)]
//        public double AvgRating { get; set; }
//        public string EventImages { get; set; } = "[]";
//        public int EventId { get; set; }
//        public Event Event { get; set; }
//        public List<Stage>? Stages { get; set; }=new List<Stage>();
//        public List<EventSeat> EventSeats { get; set; } = new List<EventSeat>();
//    }
//}
