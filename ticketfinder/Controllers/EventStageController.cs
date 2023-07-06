using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.DTO.EventStageDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStageController : ControllerBase
    {

        TicketFinderContext context;
        public EventStageController()
        {
            context = new TicketFinderContext();
        }


        [HttpGet]
        public IActionResult GetStages()
        {
            if (context.EventStages == null) return NotFound();
            List<EventStage> eventStages = context.EventStages.Include(st => st.EventSeats).Include(st => st.Stage).ToList();
            //stages.ForEach(s => s.SetSeats(s.Capacity, s.CapacityVip)); later

            return Ok(eventStages);

        }

        [HttpGet("{id}")]
        public IActionResult GetStages(int id)
        {
            var eventStage = context.EventStages.Include(es => es.EventSeats).Include(es => es.Stage).AsQueryable().FirstOrDefault(es=>es.Id==id);
            if (eventStage == null) return NotFound();
            return Ok(eventStage);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStage(int id)
        {

            if (id == null) return BadRequest("id can not be null");

            var eventStage = context.EventStages.Include(e=>e.EventSeats).FirstOrDefault(e => e.Id == id);
            if (eventStage == null) return NotFound();

            var evetSeats = eventStage.EventSeats.ToList();

            if( evetSeats.Count>0)
            {
                context.EventSeats.RemoveRange(evetSeats);
            }
            context.EventStages.Remove(eventStage);

            context.SaveChanges();


            return Ok("Not implemented yet");
        }


        [HttpPost]
        public IActionResult CreateEventStage(CreateEventStageDTO model)
        {
            if (!ModelState.IsValid||model.EventId==null||model.StageId==null)
            {
                return BadRequest(ModelState);
            }

            Stage stage = context.Stages.Include(s=>s.Seats).FirstOrDefault(s=>s.Id==model.StageId);
            if (stage==null)
            {
                return BadRequest("Stage is not defined!");
            }
            Event @event=context.Events.Find(model.EventId);
            if (@event==null)
            {
                return BadRequest("Event is not defined!");
            }

            EventStage eventStage=new EventStage();

            eventStage.BasePrice = @event.Price;
            eventStage.Stage = stage;
            eventStage.EventId= model.EventId;
            eventStage.Name = @event.Name;

            List<Seat> seats = stage.Seats;
            eventStage.SetSeats(seats);
            context.EventStages.Add(eventStage);

            context.SaveChanges();




            return Ok(eventStage);
        }
    }
}
