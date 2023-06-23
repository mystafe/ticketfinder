using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            if (events.Count == 0)
            {
                return NotFound();

            }
            return Ok(events);
        }


        [HttpGet("{id}")]
        public IActionResult GetEvent(int id)
        {

            Event myEvent = context.Events.Include(e => e.EventStages).Include(e => e.EventImages).AsQueryable().SingleOrDefault(e => e.Id == id);
            if (myEvent == null)
            {
                return NotFound();

            }

            ResponseEventDTO responseEvent = new ResponseEventDTO();
            responseEvent.Id = myEvent.Id;
            responseEvent.EventStages = myEvent.EventStages;
            responseEvent.EventType = myEvent.EventType;
            responseEvent.Duration = myEvent.Duration;
            responseEvent.IsOnSale = myEvent.IsOnSale;
            responseEvent.IsActive = myEvent.IsActive;
            responseEvent.IsAvailable = myEvent.IsAvailable;
            responseEvent.Date = myEvent.Date;
            responseEvent.Name = myEvent.Name;
            responseEvent.Price = myEvent.Price;
            responseEvent.EventImages = myEvent.EventImages;

            return Ok(responseEvent);
        }

        [HttpPost]
        public IActionResult CreateEvent(CreateEventDTO eventmodel)
        {
            if (eventmodel == null)
            {
                return BadRequest();

            }

            Event newEvent = new Event();
            newEvent.Name = eventmodel.Name;
            newEvent.Date = eventmodel.Date;


            newEvent.EventType = eventmodel.EventType;
            newEvent.Duration = eventmodel.Duration;
            newEvent.Price = eventmodel.Price;

            var eventImageList = eventmodel.EventImages.Split(',').ToList();
            newEvent.EventImages = eventImageList.Select(e => new EventImage() { UrlAddress = e, Description = e }).ToList();

            if (eventmodel.StageIds != null)
            {
                var stageIdList = eventmodel.StageIds.ToList();
                newEvent.EventStages = null; 
                    
                var myStages= context.Stages.Include(s => s.Seats)
                    .Where(s => stageIdList.Contains(s.Id)).ToList();
                var myEventStages = context.EventStages.Include(es => es.EventSeats).Where(es => myStages.Contains(es.Stage)).ToList();
                
                newEvent.EventStages = myEventStages;


            }
            else newEvent.EventStages = null;
            context.Events.Add(newEvent);
            context.SaveChanges();
            return Ok(newEvent);
        }

    }
}
