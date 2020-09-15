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
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoomRepository repository;

        public HotelRoomsController(IHotelRoomRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/HotelRooms
        [HttpGet]
        public async Task<IEnumerable<HotelRoom>> GetHotelRooms()
        {
            return await repository.GetAllAsync();
        }

        // GET: api/HotelRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(long id)
        {
            var hotelRoom = await repository.GetOneByIdAsync(id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRoom(long id, HotelRoom hotelRoom)
        {
            if (id != hotelRoom.Id)
            {
                return BadRequest();
            }

            bool didUpdate = await repository.UpdateAsync(hotelRoom);

            if (!didUpdate)
                return NotFound();

            return NoContent();
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
            await repository.CreateAsync(hotelRoom);

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.Id }, hotelRoom);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(long id)
        {
            var hotelRoom = await repository.DeleteAsync(id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }
    }
}
