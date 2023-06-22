using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.DTO.PlaceDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        TicketFinderContext context;
        public PlaceController()
        {
            context = new TicketFinderContext();
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Place> places = context.Places.Include(p=>p.Address).Include(p=>p.Stages).ToList();
            if (places.Count == 0)
            {
                return NotFound();
            }
            return Ok(places);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var place = context.Places.Include(p=>p.Stages).Include(p=>p.Address).AsQueryable().FirstOrDefault(x => x.Id == id);
            if (place == null)
            {
                return NotFound();
            }
            return Ok(place);
        }

        [HttpPost]
        public IActionResult CreatePlace([FromBody]  CreatePlaceDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var place = new Place();
            place.Name= model.Name;
           var address =context.Addresses.FirstOrDefault(a => a.Id == model.AddressId);
            if (address == null)
            {
                return BadRequest("address is not valid!");   
            }
            place.Address = address;
            context.Places.Add(place);
            context.SaveChanges();
            return Ok(place);
        }
    }
}
