//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using System.Xml.Linq;
//using ticketfinder.Context;

//namespace ticketfinder.Models.ORM
//{
//    public class Stage
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public bool IsIndoor { get; set; } = true;
//        public int PlaceId { get; set; }
//        public Place Place { get; set; }
//        public List<Seat>? Seats { get; set; }
//        public List<EventDetail> EventDetails { get; set; } = new List<EventDetail>();

//        public int Capacity { get; set; }
//        public int CapacityNormal { get; set; }
//        public int CapacityVip { get; set; }

//        public List<Seat> SetSeats(int capacity=10,int vip=2)
//        {

//            List<Seat> seats = new List<Seat> { };

//            if (vip > 0)
//            {
//                for (int i = 1; i <= capacity; i++)
//                {
//                    Seat seat = new Seat();

//                    if (i <= vip)
//                    {
//                        seat.Name = "Vip " + i;
//                        seat.Type = SeatType.Vip;
//                    }
//                    else
//                    {
//                        seat.Name = "Seat " + i;
//                        seat.Type = SeatType.Normal;

//                    }
//                    seats.Add(seat);
//                }
//            }
//            else
//            {
//                for (int i = 1; i <= capacity; i++)
//                {
//                    Seat seat = new Seat();
//                    if (i % 5 == 0)
//                    {

//                        seat.Name = "Vip " + i % 5;
//                        seat.Type = SeatType.Vip;
                                      
//                    }
//                    else
//                    {
//                        seat.Name = "Seat " + i;
//                        seat.Type = SeatType.Normal;
                       
//                    }
//                    seats.Add(seat);
//                }

//            }

//            Seats = seats;
//            Capacity = capacity;
//            CapacityNormal = capacity - vip;
//            CapacityVip = vip;
          
//            return seats;

//        }
    
//    }
//}