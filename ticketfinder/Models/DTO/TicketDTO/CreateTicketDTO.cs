namespace ticketfinder.Models.DTO.TicketDTO
{
    public class CreateTicketDTO
    {

        public DateTime? DateOfPurchase { get; set; }=DateTime.Now;
        public int CustomerId { get; set; }
        public int EventSeatId { get; set; }

    }
}
