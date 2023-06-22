﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public IActionResult CreateEventStage(CreateEventStageDTO model)
        {
            if (!ModelState.IsValid||model==null||model.StageId==null||model.BasePrice==null)
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

            eventStage.BasePrice = model.BasePrice;
            eventStage.Stage = stage;
            eventStage.EventId= model.EventId;

            List<Seat> seats = stage.Seats;
            eventStage.SetSeats(seats);
            context.EventStages.Add(eventStage);

            context.SaveChanges();



            return Ok(eventStage);
        }
    }
}
