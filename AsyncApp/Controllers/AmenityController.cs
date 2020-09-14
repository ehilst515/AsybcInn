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

        public AmenityController(IAmenityRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Amenity
        [HttpGet]
        public async Task<IEnumerable<Amenity>> GetAmenity()
        {
            return await repository.GetAllAmenities();

        }

        // GET: api/Amenity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amenity>> GetAmenity(long id)
        {
            var Amenity = await repository.GetAmenity(id);

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
        public async Task<IActionResult> PutAmenity(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }
            bool didUpdate = await repository.UpdateAmenity(amenity);
            if (didUpdate == false)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Amenity
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity amenity)
        {
            await repository.CreateAmenity(amenity);

            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: api/Amenity/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amenity>> DeleteAmenity(long id)
        {
            Amenity amenity = await repository.DeleteOneAmenityById(id);
  
            if (amenity == null)
            {
                return NotFound();
            }

            return amenity;
        }
    }
}
