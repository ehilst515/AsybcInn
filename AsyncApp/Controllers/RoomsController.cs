using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AsyncApp.Models;
using AsyncApp.Services;

namespace AsyncApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository repository;


        public RoomsController(IRoomRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await repository.GetAllAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(long id)
        {
            var room = await repository.GetOneRoomByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(long id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }
            bool didUpdate = await repository.UpdateOneRoom(room);
            if (didUpdate == false)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            await repository.CreateRoom(room);

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(long id)
        {
            var room = await repository.DeleteOneRoomById(id);
            if (room == null)
            {
                return NotFound();
            }

               return room;
        }

        [HttpPost("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult<Amenity>> AddAmenityToRoom(long roomId, long amenityId)
        {
            await repository.AddAmenityToRoom(roomId, amenityId);
            return CreatedAtAction(nameof(AddAmenityToRoom), new { roomId, amenityId }, null);
        }
        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult<Amenity>> DeleteAmenityFromRoom(long roomId, long amenityId)
        {
            await repository.DeleteAmenityFromRoom(roomId, amenityId);
            return Ok();
        }

    }
}
