using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketfinder.Context;
using ticketfinder.Models.DTO.EventDetailDTO;
using ticketfinder.Models.ORM;

namespace ticketfinder.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class EventDetailController : ControllerBase
    {


        TicketFinderContext context;
        public EventDetailController()
        {
            context = new TicketFinderContext();
        }

        [HttpGet]
        public IActionResult GetEventDetails()
        {
            List<ResponseEventDetailDTO> eventdetails = context.EventDetails.Include(ed => ed.Stages).Include(ed => ed.Event).Select(ed => new ResponseEventDetailDTO()
            {
                Id = ed.Id,
                Name = ed.Name,
                StageNames = ed.Stages.Select(s => s.Name).ToArray(),
                AllSeats = ed.EventSeats,
                AvailableSeats = ed.EventSeats.Where(es => es.IsOnSale == true && es.IsSold == false).ToList(),
                AvgRating = ed.AvgRating,
                Duration = ed.Duration,
                Date = ed.Date,
                EventImages = ed.EventImages,
                EventName = ed.Event.Name,
                IsActive = ed.IsActive,
                IsAvailable = ed.IsAvailable,
                IsOnSale = ed.IsOnSale,

            }).ToList();



            if (eventdetails.Count > 0)
            {
                return Ok(eventdetails);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetEventDetail(int id)
        {
            EventDetail ed = context.EventDetails.Include(ed => ed.Stages)
                                                 .Include(ed => ed.Event)
                                                 .Include(ed => ed.EventSeats)
                                                 .AsQueryable()
                                                 .FirstOrDefault(ed => ed.Id == id);
            if (ed != null)
            {
                ResponseEventDetailDTO result = new ResponseEventDetailDTO()
                {
                    Id = ed.Id,
                    Name = ed.Name,
                    StageNames = ed.Stages?.Select(s => s.Name).ToArray(),
                    AllSeats = ed.EventSeats,
                    AvailableSeats = ed.EventSeats.Where(es => es.IsOnSale == true && es.IsSold == false).ToArray() ?? null,
                    AvgRating = ed.AvgRating,
                    Duration = ed.Duration,
                    Date = ed.Date,
                    EventImages = ed.EventImages,
                    EventName = ed.Event.Name,
                    IsActive = ed.IsActive,
                    IsAvailable = ed.IsAvailable,
                };
                return Ok(result);
            }
            else
            {
                return NotFound();
            }


        }

        [HttpPost]
        public IActionResult createEventDetail(CreateEventDetailDTO model)
        {


            if (!ModelState.IsValid || model == null)
            {
                return BadRequest();
            }

            EventDetail ed = new EventDetail();
            EventSeat es = new EventSeat();

            ed.Name = model.Name;

            ed.Date = model.Date != null ? model.Date : DateTime.Now;


            ed.Duration = model.Duration != null ? model.Duration : TimeSpan.Zero;

            ed.EventId = model.EventId;

            ed.EventImages = model.EventImages;



            model.StageIds.ForEach(si =>
            {
                Stage stage = context.Stages.Find(si);
                if (stage != null)
                {
                    ed.Stages.Add(stage);
                    stage.Seats.ForEach(se =>
                        {

                            EventSeat eventSeat = new EventSeat();
                            es.SeatId = se.Id;
                            es.EventDetailId = ed.EventId;
                            es.Price = se.Type == SeatType.Vip ? model.Price
                                : se.Type == SeatType.Normal ? model.Price * 0.8M
                                : model.Price * 0.6M;

                            ed.EventSeats.Add(eventSeat);
                        }

                    );
                }
            });


            context.EventDetails.Add(ed);
            context.SaveChanges();

            return Ok(ed);
        }
    }
}

