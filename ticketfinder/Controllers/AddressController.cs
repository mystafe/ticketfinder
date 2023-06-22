using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ticketfinder.Context;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        TicketFinderContext context;
        public AddressController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results=context.Addresses.ToList();

            if (results.Count==0)
            {
                return NotFound();
                
            }
            return Ok(results);
        }
    }
}
