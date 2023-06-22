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
            List<EventSeat> eventSeats = context.EventSeats.Include(es=>es.Seat).ToList();

            if (eventSeats==null)
            {
                return NotFound();
            }
            return Ok(eventSeats);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventSeatById(int id)
        {
            var eventSeat = context.EventSeats.Include(es => es.Seat).AsQueryable().FirstOrDefault(es=>es.Id == id);

            if (eventSeat == null)
            {
                return NotFound();
            }
            return Ok(eventSeat);
        }


    }
}
