using System.Xml.Linq;
using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class Place
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public TimeSpan OpenHour { get; set; }
        public TimeSpan CloseHour { get; set; }
        public bool IsActive { get; set; }
        public List<Stage>? Stages  { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
