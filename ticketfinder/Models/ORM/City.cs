using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class City
    {
        
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CountryId { get; set; }
        public Country? Country { get; set; }
        public ICollection<Place>? Places { get; set; }
        public ICollection<Address>? Addresses { get; set; }


    }
}