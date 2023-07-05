using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class Address
    {
        public int Id { get; set; }
        public string FullAddress { get; set; }
        public string GeoLocation { get; set; }
        public  City? City { get; set; }

    }
}