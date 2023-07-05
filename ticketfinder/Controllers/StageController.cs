using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.DTO.StageDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {

        TicketFinderContext context;
        public StageController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult GetStages()
        {
            if (context.Stages == null) return NotFound();
            List<Stage> stages = context.Stages.Include(st=>st.Seats).Include(st=>st.Place).ToList();
            return Ok(stages);
        }
        [HttpGet("{id}")]
        public IActionResult GetStagesById(int id)
        {
            var stage = context.Stages.Include(s => s.Seats).AsQueryable().FirstOrDefault(s => s.Id == id);
            if (stage == null) return NotFound();
            return Ok(stage);
        }

        [HttpPost]
        public IActionResult PostStages(CreateStageDTO model)
        {
            Stage stage = new Stage();
            stage.Name = model.Name;
            stage.Capacity = model.Capacity;
            stage.CapacityVip = model.CapacityVip;
            stage.CapacityNormal = model.CapacityNormal;
            stage.IsIndoor = model.IsIndoor;
            var place = context.Places.Find(model.PlaceId);
            if (place==null)
            {
                return BadRequest("Place is not defined!");   
            }
            stage.Place = place;

            List<Seat> seats = new List<Seat>();

            if (model.Capacity>0|| model.CapacityVip>0||model.CapacityNormal>0)
            {
                for (int i = 1; i <= model.CapacityVip; i++)
                {
                    seats.Add(new Seat
                    {
                        Name = "Vip " + i,
                        Stage = stage,
                        Type = SeatType.Vip
                    });
                }
                for (int i = 1; i <= model.CapacityNormal; i++)
                {
                    seats.Add(new Seat
                    {
                        Name = "Normal " + i,
                        Stage = stage,
                        Type = SeatType.Normal
                    });
                }

                if (model.Capacity> (model.CapacityVip+model.CapacityNormal))
                {
                    for(int i = 1;i <= model.Capacity-(model.CapacityNormal+model.CapacityVip); i++)
                    {
                        seats.Add(new Seat
                        {
                            Name = "Seat " + i,
                            Stage = stage,
                            Type = SeatType.Normal
                        });

                    }
                    
                }



            }
            stage.SetSeats(seats);// .SetSeats(model.Capacity, model.CapacityVip);
            context.Stages.Add(stage);
            context.SaveChanges();
            return Ok(stage);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStage(int id)
        {
            if (id == null) return BadRequest("id can not be null");
            var stage = context.Stages.Include(s => s.Place).Include(s => s.Seats).FirstOrDefault(s => s.Id == id);
            if (stage == null) return NotFound();
            var seats = stage.Seats;       
            var eventStages = context.EventStages.Include(es => es.Stage).AsQueryable().Where(es => es.Stage.Id == stage.Id).ToList();
            var eventStagesIds = eventStages.Select(es => es.Id).ToList();
            var eventSeats = context.EventSeats.AsEnumerable()
                .Where(ev => eventStagesIds.Contains(ev.EventStageId))
                .ToList();
            var events = context.Events
                .Include(e => e.EventStages)
                .Where(e => e.EventStages.Any(es => eventStagesIds.Contains(es.Id)))
                .ToList();
             var eventIds = events.Select(ev => ev.Id);
            var eventImages = context.EventImages.Where(ei => eventIds.Contains((int)ei.EventId)).ToList();
            var ratings = context.Ratings.Include(r => r.Event).Where(r => eventIds.Contains(r.Event.Id)).ToList();
            if (eventImages.Any())
            {
                context.EventImages.RemoveRange(eventImages);
            }
            if (ratings.Any())
            {
                context.Ratings.RemoveRange(ratings);
            }
            if (eventSeats.Any())
            {
                context.EventSeats.RemoveRange(eventSeats);
            }
            if (eventStages.Any())
            {
                context.EventStages.RemoveRange(eventStages);
            }

            if (events.Any())
            {
                context.Events.RemoveRange(events);
            }
            if (seats.Any())
            {
                context.Seats.RemoveRange(seats);
            }
            context.Stages.RemoveRange(stage);            
            context.SaveChanges();
            return Ok(stage);
        }
    }
}
