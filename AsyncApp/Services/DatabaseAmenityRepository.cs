using AsyncApp.Data;
using AsyncApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncApp.Services
{
    public interface IAmenityRepository
    {
        Task<IEnumerable<Amenity>> GetAllAsync();
    }

    public class DatabaseAmenityRepository : IAmenityRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseAmenityRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Amenity>> GetAllAsync()
        {
            return await _context.Amenity.ToListAsync();
        }

    }
}
