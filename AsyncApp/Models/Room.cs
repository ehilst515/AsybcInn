using System.Collections.Generic;

namespace AsyncApp.Models
{
    public class Room
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Layout { get; set; }

        public List<RoomAmenity> RoomAmenities { get; set; }

    }
}
