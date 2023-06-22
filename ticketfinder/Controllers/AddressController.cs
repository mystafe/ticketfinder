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
            var results=context.Addresses.ToList();

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
            address.FullAdress = model.FullAdress;     
            address.City = city;
            address.GeoLocation = model.GeoLocation;
            context.Addresses.Add(address);
            return Ok(address);
        }
    }
}
