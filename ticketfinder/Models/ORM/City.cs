using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class City
    {
        
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual Country? Country { get; set; }
        public List<Place>? Places { get; set; }
        public List<Address>? Addresses { get; set; }


    }
}