using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        TicketFinderContext context;
        public SeatController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult GetEventSeat()
        {
            List<Seat> seats = context.Seats.Include(es=>es.Stage).ToList();

            if (seats==null)
            {
                return NotFound();
            }
            return Ok(seats);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventSeatById(int id)
        {
            var seat = context.Seats.Include(es => es.Stage).AsQueryable().FirstOrDefault(es=>es.Id == id);

            if (seat == null)
            {
                return NotFound();
            }
            return Ok(seat);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSeat(int id)
        {
            if (id == null) return BadRequest("Id can not be null!");

            var seat = context.Seats.Include(s => s.Stage).FirstOrDefault(s => s.Id == id);
            if (seat == null) return NotFound();

            var eventSeats = context.EventSeats.Include(e => e.Seat).AsQueryable()
                .Where(es => es.Seat.Id == seat.Id).ToList();

            var eventSeatIds = eventSeats.Select(e => e.Id);

            // var tickets = context.Tickets.AsQueryable()
            //   .Where(t => eventSeatIds.Contains(t.EventSeatId)).ToList();

            //business approach??

            if (eventSeats.Count>0)
            {
                context.EventSeats.RemoveRange(eventSeats);
            }
            context.Seats.Remove(seat);
            context.SaveChanges();
            

            return Ok(seat);
        }

    }
}
