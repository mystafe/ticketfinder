using ticketfinder.Context;

namespace ticketfinder.Models.ORM
  
{
    public enum SeatType
    {
        Normal,
        Vip
    }
    public class Seat
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public SeatType Type { get; set; } = SeatType.Normal;
        public Stage? Stage { get; set; }
 
    }
}