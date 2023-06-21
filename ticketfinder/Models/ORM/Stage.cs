using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Linq;
using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class Stage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIndoor { get; set; } = true;
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public List<Seat>? Seats { get; set; }
        public List<EventDetail>? EventDetails { get; set; }

        public int Capacity { get; set; } = 100;
        public int CapacityNormal { get; set; } = 80;
        public int CapacityVip { get; set; } = 20;



        public Stage(int PlaceId, int Capacity,string Name="stage1")
        {
            Name = Name;
            IsIndoor = IsIndoor;
            PlaceId = PlaceId;
           
            for(int i = 1; i <= Capacity; i++)
            {
                Seat seat = new Seat();
                if (i%5==0)
                {
                    
                    seat.Name = Name;
                    seat.Type = SeatType.Vip;
                    seat.StageId = this.Id;                    
                }
                else
                {
                    seat.Name = Name;
                    seat.Type = SeatType.Normal;
                    seat.StageId = this.Id;
                }
                Seats.Add(seat);

            }
            EventDetails = new List<EventDetail>();
           

        }
    }
}