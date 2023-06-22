using Microsoft.AspNetCore.Mvc;
using ticketfinder.Context;
using ticketfinder.Models.DTO.EventDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {


        TicketFinderContext context;
        public EventController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            List<ResponseEventDTO> events = context.Events.Select(e =>
                new ResponseEventDTO()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Price = e.Price,
                    Date = e.Date,
                    AvgRating = e.AvgRating,
                    EventType = e.EventType,
                    Duration = e.Duration,
                    EventImages = e.EventImages,
                    EventStages = e.EventStages,
                    IsActive = e.IsActive,
                    IsAvailable = e.IsAvailable,
                    IsOnSale = e.IsOnSale,
                }).ToList();

            if (events.Count==0)
            {
                return NotFound();
                
            }
            return Ok(events);
        }

        [HttpPost]
        public IActionResult CreateEvent(CreateEventDTO eventmodel)
        {
            if (eventmodel==null)
            {
                return BadRequest();
                
            }

            Event newEvent=new Event();
            newEvent.Name = eventmodel.Name;
            newEvent.Date = eventmodel.Date;

            List <EventStage> eventStages=new List<EventStage>();
           // newEvent.EventStages = eventStages;//fix will come
            newEvent.EventType = eventmodel.EventType;
            newEvent.Duration = eventmodel.Duration;
            //newEvent.EventImages = eventmodel.EventImages;
            newEvent.Price = eventmodel.Price;

            context.Events.Add(newEvent);
            context.SaveChanges();


            return Ok(newEvent);
        }

    }
}
