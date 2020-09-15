using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncApp.Models
{
    public class HotelRoom
    {
        public long Id { get; set; }
        public long HotelId { get; set; }
        public long RoomId { get; set; }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }


        public long RoomNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }
    }
}
