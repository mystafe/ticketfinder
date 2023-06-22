using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.EventStageDTO
{
    public class CreateEventStageDTO
    {
        public double BasePrice { get; set; }
        public int StageId { get; set; }
        public int EventId { get; set; }
       
    }
}
