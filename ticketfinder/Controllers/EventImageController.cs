using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ticketfinder.Context;
using ticketfinder.Models.DTO.EventImageDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventImageController : ControllerBase
    {

        TicketFinderContext context;
        public EventImageController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results = context.EventImages.ToList();

            if (results.Count == 0)
            {
                return NotFound();

            }
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = context.EventImages.FirstOrDefault(x => x.Id == id);

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(CreateEventImageDTO model )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (model==null)
                return BadRequest();
            EventImage eventImage = new EventImage();

            eventImage.Description = model.Description;
            eventImage.UrlAddress = model.UrlAddress;
            context.EventImages.Add(eventImage);

            return Ok(eventImage);
            

        }
    }
}
