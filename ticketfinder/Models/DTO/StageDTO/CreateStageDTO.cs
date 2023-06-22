using ticketfinder.Models.ORM;

namespace ticketfinder.Models.DTO.StageDTO
{
    public class CreateStageDTO
    {

        public string Name { get; set; } = "new stage";
        public bool IsIndoor { get; set; } = true;
        public int PlaceId { get; set; }
        public int Capacity { get; set; }
        public int CapacityNormal { get; set; }
        public int CapacityVip { get; set; }

    }
}
