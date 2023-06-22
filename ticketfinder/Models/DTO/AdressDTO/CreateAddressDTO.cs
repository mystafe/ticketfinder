using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.AdressDTO
{
    public class CreateAddressDTO
    {
        public int Id { get; set; }
        public string FullAdress { get; set; }
        public string GeoLocation { get; set; }

        public int CityId  { get; set; }
    }
}
