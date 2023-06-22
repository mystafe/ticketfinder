using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.StageDTO
{
    public class ResponseStageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIndoor { get; set; }
        public int Capacity { get; set; }
        public int CapacityNormal { get; set; }
        public int CapacityVip { get; set; }
        public Place Place { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
