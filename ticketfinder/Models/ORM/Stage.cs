using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Linq;
using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class Stage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsIndoor { get; set; } = true;
        public int Capacity { get; set; }
        public int CapacityNormal { get; set; }
        public int CapacityVip { get; set; }

        public int PlaceId { get; set; }
        public virtual Place? Place { get; set; }
        public List<Seat>? Seats { get; set; }


        public void SetSeats(List<Seat> seats)
        {

            Seats = seats;

            Capacity = seats.Count;
            CapacityNormal = seats.Where(s => s.Type == SeatType.Normal).Count();
            CapacityVip = seats.Where(s => s.Type == SeatType.Vip).Count();

        }

    }
}