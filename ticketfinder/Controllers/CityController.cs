using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.DTO.CityDTO;
using ticketfinder.Models.DTO.EventStageDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        TicketFinderContext context;
        public CityController()
        {
            context = new TicketFinderContext();
        }


        [HttpGet]
        public IActionResult GetCities()
        {
            if (context.Cities == null) return NotFound();

            var cities = context.Cities.Include(c=>c.Country);

            return Ok(cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city=context.Cities.Include(c=>c.Country).FirstOrDefault(c => c.Id == id);
            
            if (city==null) return NotFound();

            return Ok(city);
            
        }

        [HttpPost]
        public IActionResult Create(CreateCityDTO model)
        {
            if (!ModelState.IsValid||model.CountryId==null||model.Name==null)
            {
                return BadRequest();
            }

            City city=new City();
            city.Name=model.Name;
            city.CountryId=model.CountryId;
            context.Cities.Add(city);
            context.SaveChanges();
            return Ok(city);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {

            if (id == null) return BadRequest("id can not be null!");

            var city = context.Cities.Find(id);
            if (city == null) return NotFound();

            var address = context.Addresses.Include(a=>a.City).FirstOrDefault(c => c.City.Id == id);

            if (address == null)
            {
                context.Cities.Remove(city);
                context.SaveChanges();
                return Ok(city);
                ;
            }
            else
            {
                return BadRequest(" There is an adress relation for the related city!" + address.FullAddress + " , " + city.Name);
            }
        }


    }
}
