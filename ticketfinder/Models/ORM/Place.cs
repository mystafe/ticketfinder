using System.Xml.Linq;
using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class Place
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public TimeSpan? OpenHour { get; set; }
        public TimeSpan? CloseHour { get; set; }
        public bool IsActive { get; set; }
        public virtual Address? Address { get; set; }
        public virtual City? City  { get; set; }
        public List<Stage>? Stages  { get; set; }
    }
}
