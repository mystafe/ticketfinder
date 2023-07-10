using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.DTO;
using ticketfinder.Models.DTO.CountryDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        TicketFinderContext context;
        public RatingController()
        {
            context = new TicketFinderContext();
        }


        [HttpGet]
        public IActionResult GetRatings()
        {
            if (context.Ratings == null) return NotFound();

            var ratings = context.Ratings.Include(r=>r.Customer).Include(r=>r.Event).AsQueryable().ToList();

            return Ok(ratings);
        }

        [HttpGet("{id}")]
        public IActionResult GetRating(int id)
        {

            Rating rating = context.Ratings.Include(c => c.Customer).AsQueryable().FirstOrDefault(c => c.Id == id);
            if (rating == null)
                return NotFound();
            return Ok(rating);
        }

        [HttpPost]
        public IActionResult CreateCountry(CreateRatingDTO model)
        {
            if (!ModelState.IsValid || model.CustomerId == null||model.EventId==null||model.RatingValue==null) return BadRequest();

            Rating rating = new Rating();
            rating.RatingValue = model.RatingValue;
            rating.Comment = model.Comment;
            Customer customer=context.Customers.FirstOrDefault(c => c.Id==model.CustomerId);
            if (customer == null)  return BadRequest("Customer is not found!");
            Event @event = context.Events.FirstOrDefault(c => c.Id == model.EventId);
            if (@event == null) return BadRequest("Event is not found!");

            rating.Customer = customer;
            rating.Event = @event;


            //Check whether the customer participate that event or not. Also it needs to be after event time..
          
            var avgRat= @event.AvgRating;
            var totRankList = context.Ratings.Include(r => r.Event)
                .Where(r => r.Event.Id == @event.Id).ToList();
            var totRank = totRankList.Count();
            var totalRatingForEvent = avgRat * totRank;
            totalRatingForEvent += rating.RatingValue;
            var newAvg = totalRatingForEvent / (totRank + 1);
            @event.AvgRating = newAvg;
            context.Ratings.Add(rating);
            context.SaveChanges();



            return Ok(rating);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteRating(int id) {

            if (id == null) return BadRequest("Id can not be null");
            var rating = context.Ratings.FirstOrDefault(r => r.Id == id);

            if (rating == null) return NotFound();

            context.Ratings.Remove(rating);
            context.SaveChanges();


            return Ok(rating);
        }


    }
}
