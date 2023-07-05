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

            if (eventmodel.EventImages == "")
                eventmodel.EventImages = "https://acs.digitellinc.com/assets/images/image_placeholder.jpg";

            var eventImageList = eventmodel.EventImages.Split(',').ToList();
            newEvent.EventImages = eventImageList.Select(e => new EventImage() { UrlAddress = e, Description = e }).ToList();


            List<Stage> myStages= new List<Stage>();

            if (eventmodel.StageIds != null)
            {
                var stageIdList = eventmodel.StageIds.ToList();
                

      
                 myStages= context.Stages.Include(s => s.Seats).AsQueryable()
                    .Where(stage => stageIdList.Contains(stage.Id)).ToList();


                //newEvent.SetEventStage(myStages);

            }
            else  if (eventmodel.StageIds == null) newEvent.EventStages = null;


            context.Events.Add(newEvent);
            context.SaveChanges();

            var event3 = context.Events.Include(e=>e.EventStages).FirstOrDefault(e => e.Name == newEvent.Name);

            event3.SetEventStage(myStages);
            context.SaveChanges();
            
            return Ok(newEvent);
        }

    }
}
