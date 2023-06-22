using System.ComponentModel.DataAnnotations;
using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.EventDetailDTO
{
    public class CreateEventDetailDTO
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        public TimeSpan Duration { get; set; }= TimeSpan.Zero;
        
        public int EventId { get; set; }
        public List<string>? EventImages { get; set; }
        public List<int>? StageIds { get; set; }
        public decimal Price { get; set; }



    }
}
