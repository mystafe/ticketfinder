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
            //stages.ForEach(s => s.SetSeats(s.Capacity, s.CapacityVip)); later

            return Ok(stages);

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
    }
}
