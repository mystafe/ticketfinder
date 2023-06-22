using System.ComponentModel.DataAnnotations;
using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.EventDetailDTO
{
    public class ResponseEventDetailDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; }
        public bool IsOnSale { get; set; }
        public bool IsAvailable { get; set; }

        [MinLength(0)]
        [MaxLength(5)]
        public double AvgRating { get; set; }
        public List<string>? EventImages { get; set; }
        public string EventName { get; set; }
        public List<string>? StageNames { get; set; }
        public ICollection<EventSeat>? AllSeats { get; set; }
        public ICollection<EventSeat>? AvailableSeats { get; set; }


    }
}
