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
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
    }


    public class DatabaseHotelRepository: IHotelRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotel.ToListAsync();
        }

    }
}
