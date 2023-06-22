using System.Xml.Linq;
using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public enum CustomerType
    {
        Normal=1,
        Student,
        Disabled,
        Child,
        Old
    }
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
        public CustomerType CustomerType { get; set; }= CustomerType.Normal;
        public virtual Address? Address { get; set; }
        public List<Rating>? Ratings { get; set; }
        public List<Ticket>? Tickets { get; set; }

    }
}