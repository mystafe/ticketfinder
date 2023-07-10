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
            //List<ResponseEventDTO> events = context.Events.Include(e=>e.EventStages).Include(e=>e.EventImages).Select(e =>
            //    new ResponseEventDTO()
            //    {
            //        Id = e.Id,
            //        Name = e.Name,
            //        Price = e.Price,
            //        Date = e.Date,
            //        AvgRating = e.AvgRating,
            //        EventType = e.EventType,
            //        Duration = e.Duration,
            //        Description=e.Description,
            //        EventImages = e.EventImages,
            //        EventStages = e.EventStages,
            //        IsActive = e.IsActive,
            //        IsAvailable = e.IsAvailable,
            //        IsOnSale = e.IsOnSale,

            //    }).ToList();
            //var allEventSeats = context.EventSeats;

            List<Event> events = context.Events.Include(e => e.EventStages).Include(e => e.EventImages).ToList();

            if (events.Count == 0)
            {
                return NotFound();

            }
            return Ok(events);
        }

        [HttpGet("deleteAll")]
        public IActionResult DeleteAll()
        {
            var ei = context.EventImages.ToList();
            if (ei.Count > 0) context.EventImages.RemoveRange(ei);
            var tc = context.Tickets.ToList();
            if (tc.Count > 0) context.Tickets.RemoveRange(tc);

            var evse = context.EventSeats.ToList();
            if (evse.Count > 0) context.EventSeats.RemoveRange(evse);
            var evst = context.EventStages.ToList();
            if (evst.Count > 0) context.EventStages.RemoveRange(evst);


            var rt = context.Ratings.ToList();
            if (rt.Count > 0) context.Ratings.RemoveRange(rt);
            var ev = context.Events.ToList();
            if (ev.Count > 0) context.Events.RemoveRange(ev);
            var se = context.Seats.ToList();
            if (se.Count > 0) context.Seats.RemoveRange(se);
            var st = context.Stages.ToList();
            if (st.Count > 0) context.Stages.RemoveRange(st);


            var pl = context.Places.ToList();
            if (pl.Count > 0) context.Places.RemoveRange(pl);
            var cu = context.Customers.ToList();
            if (cu.Count > 0) context.Customers.RemoveRange(cu);
            var adr = context.Addresses.ToList();
            if (adr.Count > 0) context.Addresses.RemoveRange(adr);

            var cit = context.Cities.ToList();
            if (cit.Count > 0) context.Cities.RemoveRange(cit);

            var cou = context.Countries.ToList();
            if (cou.Count > 0) context.Countries.RemoveRange(cou);




            context.SaveChanges();

            return Ok();
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
            responseEvent.Description = myEvent.Description;
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
            newEvent.Description = eventmodel.Description;
            newEvent.Price = eventmodel.Price;

            if (eventmodel.EventImages == "")
                eventmodel.EventImages = "https://acs.digitellinc.com/assets/images/image_placeholder.jpg";

            var eventImageList = eventmodel.EventImages.Split(',').ToList();
            newEvent.EventImages = eventImageList.Select(e => new EventImage() { UrlAddress = e, Description = e }).ToList();


            List<Stage> myStages = new List<Stage>();

            if (eventmodel.StageIds != null)
            {
                var stageIdList = eventmodel.StageIds.ToList();



                myStages = context.Stages.Include(s => s.Seats).AsQueryable()
                   .Where(stage => stageIdList.Contains(stage.Id)).ToList();


                //newEvent.SetEventStage(myStages);

            }
            else if (eventmodel.StageIds == null) newEvent.EventStages = null;


            context.Events.Add(newEvent);
            context.SaveChanges();

            var event3 = context.Events.Include(e => e.EventStages).FirstOrDefault(e => e.Name == newEvent.Name);

            event3.SetEventStage(myStages);
            context.SaveChanges();

            return Ok(newEvent);
        }




        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            if (id == null) return BadRequest("Id can not be null!");

            var @event = context.Events.Include(e => e.EventImages).Include(e => e.EventStages).AsQueryable().FirstOrDefault(e => e.Id == id);
            if (@event == null) return NotFound();

            var eventImages = context.EventImages.Where(e => e.EventId == @event.Id).ToList();

            var eventSeats = context.EventSeats.Where(e => e.EventId == @event.Id).ToList();

            var ratings = context.Ratings.Include(r => r.Event).AsQueryable()
                .Where(r => r.Event.Id == @event.Id)
                .ToList();

            var eventStages = context.EventStages.Where(e => e.EventId == @event.Id).ToList();

            if (eventImages.Count > 0)
            {
                context.EventImages.RemoveRange(eventImages);
            }
            if (ratings.Count > 0)
            {
                context.Ratings.RemoveRange(ratings);
            }
            if (eventSeats.Count > 0)
            {
                context.EventSeats.RemoveRange(eventSeats);
            }
            if (eventStages.Count > 0)
            {
                context.EventStages.RemoveRange(eventStages);
            }
            context.Events.Remove(@event);

            context.SaveChanges();


            return Ok(@event);
        }

    }
}
