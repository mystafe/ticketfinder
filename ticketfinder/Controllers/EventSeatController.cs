using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventSeatController : ControllerBase
    {
        TicketFinderContext context;
        public EventSeatController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult GetEventSeat()
        {
            List<EventSeat> eventSeats = context.EventSeats.Include(es => es.Seat).ToList();

            if (eventSeats == null)
            {
                return NotFound();
            }
            return Ok(eventSeats);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventSeatById(int id)
        {
            var eventSeat = context.EventSeats.Include(es => es.Seat).AsQueryable().FirstOrDefault(es => es.Id == id);

            if (eventSeat == null)
            {
                return NotFound();
            }
            return Ok(eventSeat);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEventSeat(int id)
        {
            if (id == null) return BadRequest("Id can not be null!");

            var eventSeat = context.EventSeats.FirstOrDefault(s => s.Id == id);
            if (eventSeat == null) return NotFound();

    

            // var tickets = context.Tickets.AsQueryable()
            //   .Where(t => t.EventSeatId==eventSeatId).ToList();

            //business approach??

      
            context.EventSeats.Remove(eventSeat);
            context.SaveChanges();
            return Ok(eventSeat);
        }
    }
}
