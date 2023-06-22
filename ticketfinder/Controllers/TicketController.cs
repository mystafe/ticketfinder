using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ticketfinder.Context;
using ticketfinder.Models.DTO.TicketDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        TicketFinderContext context;
        public TicketController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results = context.Tickets.ToList();

            if (results.Count == 0)
            {
                return NotFound();

            }
            return Ok(results);
        }

        [HttpPost]
        public IActionResult CreateTicket (CreateTicketDTO model)
        {
            

            if (model == null||model.CustomerId==null||model.EventSeatId==null)
            {
                return BadRequest();
            }


            Customer customer = context.Customers.Find(model.CustomerId);
            if (customer == null)
            {
                return BadRequest("Customer is not found!");
            }

            EventSeat eventSeat = context.EventSeats.Find(model.EventSeatId);
            if (eventSeat == null)
            {
                return BadRequest("Event Seat is not found");
            }
            Ticket ticket = new Ticket();

            double discount=0;
            switch (customer.CustomerType)
            {
                case CustomerType.Normal: discount = 0; break;
                case CustomerType.Student: discount = 0.2; break;
                case CustomerType.Old: discount = 0.2; break;
                case CustomerType.Disabled: discount = 0.5; break;
                default : discount = 0; break;
            }


            ticket.Price =  eventSeat.Price*(1-discount);
            ticket.CustomerId=customer.Id;
            ticket.EventSeatId = eventSeat.Id;
            ticket.DateOfPurchase = model.DateOfPurchase;

            context.Tickets.Add(ticket);

            customer.Tickets.Add(ticket);
            

            eventSeat.IsSold = true;

            context.SaveChanges();
            
            return Ok(ticket);
        }


    }
}
