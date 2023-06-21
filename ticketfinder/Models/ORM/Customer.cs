using System.Xml.Linq;
using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsValidated { get; set; } = false;
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

    }
}