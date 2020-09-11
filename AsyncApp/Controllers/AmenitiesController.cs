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
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityRepository repository;
        private readonly HotelDbContext _context;

        public AmenityController(IAmenityRepository repository, HotelDbContext context)
        {
            _context = context;
            this.repository = repository;
        }

        // GET: api/Amenity
        [HttpGet]
        public async Task<IEnumerable<Amenity>> GetAmenity()
        {
            return await repository.GetAllAsync();
        }

        // GET: api/Amenity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amenity>> GetAmenity(int id)
        {
            var Amenity = await _context.Amenity.FindAsync(id);

            if (Amenity == null)
            {
                return NotFound();
            }

            return Amenity;
        }

        // PUT: api/Amenity/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int id, Amenity Amenity)
        {
            if (id != Amenity.Id)
            {
                return BadRequest();
            }

            _context.Entry(Amenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Amenity
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity Amenity)
        {
            _context.Amenity.Add(Amenity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmenity", new { id = Amenity.Id }, Amenity);
        }

        // DELETE: api/Amenity/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amenity>> DeleteAmenity(int id)
        {
            var Amenity = await _context.Amenity.FindAsync(id);
            if (Amenity == null)
            {
                return NotFound();
            }

            _context.Amenity.Remove(Amenity);
            await _context.SaveChangesAsync();

            return Amenity;
        }

        private bool AmenityExists(int id)
        {
            return _context.Amenity.Any(e => e.Id == id);
        }
    }
}
