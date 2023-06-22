using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.EventSeatDTO
{
    public class CreateEventSeatDTO
    {

        public decimal EventPrice { get; set; } = 0;
        public bool IsSold { get; set; } = false;
        public bool IsOnSale { get; set; } = true;
        public int EventDetailId { get; set; }
        public int SeatId { get; set; }


    }
}
