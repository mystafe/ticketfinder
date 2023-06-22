using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class Address
    {
        public int Id { get; set; }
        public string FullAdress { get; set; }
        public string GeoLocation { get; set; }
        public virtual City? City { get; set; }

    }
}