using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.CustomerDTO
{
    public class ResponseCustomerDTO
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CityName { get; set; }
        public string? FullAdress { get; set; }



    }
}
