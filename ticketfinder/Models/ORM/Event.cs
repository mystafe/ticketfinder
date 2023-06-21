using System.ComponentModel.DataAnnotations;
using ticketfinder.Context;

namespace ticketfinder.Models.ORM
{
    public enum EventType
    {
        Concert=1,
        Festival,
        Theatre,
        Cinema,
        Sport,
        Art,
        Other=99
    }
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public EventType Type { get; set; }=EventType.Other;


    }
}