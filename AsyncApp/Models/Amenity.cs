using System.Collections.Generic;

namespace AsyncApp.Models
{
    public class Amenity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<RoomAmenity> RoomAmenities { get; set; }

    }

}
