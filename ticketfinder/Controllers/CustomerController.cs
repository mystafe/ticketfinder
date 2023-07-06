using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.DTO.CustomerDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        TicketFinderContext context;
        public CustomerController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            List<ResponseCustomerDTO> customers = context.Customers.Include(c => c.Address).Select(c => new ResponseCustomerDTO()
            {
                Id = c.Id,
                Fullname = c.Middlename != null ? c.Firstname + " " + c.Middlename + " " + c.Lastname : c.Firstname + " " + c.Lastname,
                Email = c.Email,
                FullAddress = c.Address != null ? c.Address.FullAddress : "",
                CityName = c.Address != null && c.Address.City != null ? c.Address.City.Name : "",
                Username = c.Username,
                Password = c.Password,
                Phone = c.Phone
            }).ToList();

            if (customers.Count > 0)
            {
                return Ok(customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            
            Customer customer = context.Customers.Include(c => c.Address).AsQueryable().FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                ResponseCustomerDTO result = new ResponseCustomerDTO()
                {
                    Id = customer.Id,
                    Fullname = customer.Middlename != null ? customer.Firstname + " " + customer.Middlename + " " + customer.Lastname : customer.Firstname + " " + customer.Lastname,
                    Username = customer.Username,
                    Password = customer.Password,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    FullAddress = customer.Address != null ? customer.Address.FullAddress : "",
                    CityName = customer.Address != null && customer.Address.City != null ? customer.Address.City.Name : "",
                };
                return Ok(result);
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult Post(CreateCustomerDTO model)
        {
            if (model == null)
            {
                return BadRequest("Model is not valid!");
            }
            Customer customer = new Customer();
            customer.Firstname = model.Firstname;
            customer.Middlename = model.Middlename;
            customer.Lastname = model.Lastname;
            customer.Email = model.Email;
            customer.Phone = model.Phone;
            customer.Password = model.Password;
            customer.Username = model.Username;
            if (model.AddressId!=null)
            {
                Address address = context.Addresses.FirstOrDefault(a => a.Id == model.AddressId);
                if (address==null)
                {
                    return BadRequest("Addres is not defined!");

                }

                customer.Address = address;
            }
            else
            {
                City city = context.Cities.FirstOrDefault(c => c.Id == model.CityId);
                if (city == null)
                {
                    return BadRequest("City is not found");
                }

                if (model.FullAddress == null) return BadRequest("Address is requied");
                Address address = new Address();

                address.City = city;

                address.FullAddress = model.FullAddress;
                address.GeoLocation = "[" + model.Latitude + "," + model.Longitude + "]";


                context.Addresses.Add(address);
                customer.Address = address; ;

            }

         
            context.Customers.Add(customer);
            context.SaveChanges();

            return Ok(customer);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {

            if (id == null) return BadRequest("id can not be null!");

            var customer = context.Customers.Find(id);
            if (customer == null) return NotFound();

            var rating = context.Ratings.Include(r=>r.Customer).FirstOrDefault(r => r.Customer.Id == id);
            var ticket = context.Tickets.FirstOrDefault(t => t.CustomerId == id);

            if (rating == null&&ticket==null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
                return Ok(customer);
                ;
            }
            else
            {
                return BadRequest(" There is a rating or ticket relation for the related customer! ticket:"
                    + ticket?.EventId + " / rating: "+rating?.Id +" , " + customer.Firstname+" " +customer.Lastname);
            }
        }

    }
}