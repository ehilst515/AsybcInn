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
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository repository;
        private readonly HotelDbContext _context;

        public HotelsController(IHotelRepository repository, HotelDbContext context)
        {
            _context = context;
            this.repository = repository;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<IEnumerable<Hotel>> GetHotel()
        {
            return await repository.GetAllAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(long id)
        {
            var hotel = await repository.GetOneHotelById(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(long id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }
            bool didUpdate = await repository.UpdateOneHotel(hotel);
            if (didUpdate == false)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await repository.CreateHotel(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(long id)
        {
            var hotel = await repository.DeleteOneHotelById(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }
    }
}
