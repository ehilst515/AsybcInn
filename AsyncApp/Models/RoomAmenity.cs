using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncApp.Models
{
    public class RoomAmenity
    {
        public long AmenityId { get; set; }

        public long RoomId { get; set; }


        [ForeignKey(nameof(AmenityId))]
        public Amenity Amenity { get; set; }

        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }
    }
}
