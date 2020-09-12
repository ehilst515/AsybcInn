using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncApp.Data;
using AsyncApp.Models;
using AsyncApp.Services;

namespace AsyncApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository repository;
        private readonly HotelDbContext _context;

        public RoomsController(IRoomRepository repository, HotelDbContext context)
        {
            _context = context;
            this.repository = repository;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<IEnumerable<Room>> GetRoom()
        {
            return await _context.Room.ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Room.FindAsync(id);

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
        public async Task<IActionResult> PutRoom(int id, Room room)
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
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }

        [HttpPost("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult<Amenity>> AddAmenityToRoom(int roomId, long amenityId)
        {
            await repository.AddAmenityToRoom(roomId, amenityId);
            return CreatedAtAction(nameof(AddAmenityToRoom), new { roomId, amenityId }, null);
        }
        [HttpDelete("{roomId}/Amenity/{amenityId}")]
        public async Task<ActionResult<Amenity>> DeleteAmenityFromRoom(int roomId, long amenityId)
        {
            await repository.DeleteAmenityFromRoom(roomId, amenityId);
            return Ok();
        }

    }
}
