using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.AdressDTO
{
    public class CreateAddressDTO
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string FullAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }


        public int CityId  { get; set; }
    }
}
