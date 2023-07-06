using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.DTO.AdressDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        TicketFinderContext context;
        public AddressController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results=context.Addresses.Include(a=>a.City).ToList();

            if (results.Count==0)
            {
                return NotFound();
                
            }
            return Ok(results);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {

            var result=context.Addresses.Include(a=>a.City).AsQueryable().FirstOrDefault(x => x.Id==id);
            if (result==null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CreateAddressDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model==null)
            {
                return BadRequest();
            }
            City city = context.Cities.Find(model.CityId);

            if (city==null)
            {
                return BadRequest("City is not found!");
            }
            Address address = new Address();
            address.FullAddress = model.FullAddress;     
            address.City = city;
            address.GeoLocation ="["+model.Latitude +","+model.Longitude+"]" ;
            context.Addresses.Add(address);
            context.SaveChanges();
            return Ok(address);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null) return BadRequest("Id can not be null");
            var address = context.Addresses.Include(a => a.City).AsQueryable().FirstOrDefault(a => a.Id == id);

            if (address == null) return NotFound();

            var places = context.Places.Include(p => p.Address)
                .AsQueryable()
                .Where(p => p.Address.Id == address.Id)
                .ToList();

            var customers = context.Customers.Include(c => c.Address)
                .AsQueryable()
                .Where(c => c.Address.Id == address.Id)
                .ToList();

            

            if (places.Count==0 && customers.Count==0)
            {
                context.Addresses.Remove(address);
                context.SaveChanges();
                return Ok(address);
            }



            return BadRequest("the address in use");
        }
    }
}
