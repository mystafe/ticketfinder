using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.PlaceDTO
{
    public class CreatePlaceDTO
    {

        public string Name { get; set; }
        public TimeSpan OpenHour { get; set; } = TimeSpan.FromHours(8);
        public TimeSpan CloseHour { get; set; }=TimeSpan.FromHours(20);
        public bool IsActive { get; set; } = true;
        public int? AddressId { get; set; }
        public string? FullAddress { get; set; } = "no address";
        public string? Latitude { get; set; } = "42.1";
        public string? Longitude { get; set; } = "28.1";
        public int? CityId { get; set; }


    }
}
