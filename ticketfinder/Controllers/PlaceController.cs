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
            place.Name = model.Name;
            place.OpenHour = model.OpenHour;
            place.CloseHour = model.CloseHour;
            place.IsActive = model.IsActive;


            if (model.AddressId != null)
            {



                var address = context.Addresses.FirstOrDefault(a => a.Id == model.AddressId);
                if (address == null)
                {

                    return BadRequest("Address is not found");

                }
                place.Address = address;
            }
            else if (model.CityId != null)
            {
                var city = context.Cities.Find(model.CityId);
                if (city == null) return BadRequest("City is not defined");
                var address2 = new Address();
                address2.FullAddress = model.FullAddress;
                address2.GeoLocation = "[" + model.Latitude + "," + model.Longitude + "+]";
                address2.City = city;

                context.Addresses.Add(address2);
//                var newAddress = context.Addresses.FirstOrDefault(a => a.FullAddress == model.FullAddress);
                place.Address = address2;
                place.City = city;

            }
            else return BadRequest("model is not valid");

            context.Places.Add(place);
            context.SaveChanges();
            return Ok(place);
        }
    }
}
