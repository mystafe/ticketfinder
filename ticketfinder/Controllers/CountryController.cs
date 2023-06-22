using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.DTO.CityDTO;
using ticketfinder.Models.DTO.CountryDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        TicketFinderContext context;
        public CountryController()
        {
            context = new TicketFinderContext();
        }


        [HttpGet]
        public IActionResult GetCountries()
        {
            if (context.Countries == null) return NotFound();

            var countries = context.Countries.Include(c => c.Cities).ToList();

            return Ok(countries);
        }

        [HttpGet("{id}")]
        public IActionResult GetCountry(int id)
        {
     
            Country country=context.Countries.Include(c=>c.Cities).AsQueryable().FirstOrDefault(c=>c.Id==id);
            if (country == null)
                return NotFound();
            return Ok(country);
        }

        [HttpPost]
        public IActionResult CreateCountry(CreateCountryDTO model)
        {
            if (!ModelState.IsValid || model.Name == null) return BadRequest();
            
            Country country=new Country() { Name = model.Name};
            context.Countries.Add(country);
            context.SaveChanges();

            return Ok(country);
        }
    }
}
