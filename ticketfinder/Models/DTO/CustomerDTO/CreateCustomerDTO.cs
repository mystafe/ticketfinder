using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.CustomerDTO
{
    public class CreateCustomerDTO
    {
        public string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string Lastname { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public int CityId { get; set; }
        public string? FullAdress { get; set; }
    }
}
