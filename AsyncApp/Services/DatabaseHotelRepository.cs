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
        Task<Hotel> GetOneHotelById(int id);

        Task<Hotel> CreateHotel(Hotel hotel);
        Task<Hotel> UpdateOneHotelById(int id, Hotel hotel);
        Task<Hotel> DeleteOneHotelById(int id);
    }

    public class DatabaseHotelRepository: IHotelRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public Task<Hotel> CreateHotel(Hotel hotel)
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> DeleteOneHotelById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotel.ToListAsync();
        }

        public async Task<Hotel> GetOneHotelById(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            return hotel;
        }

        public Task<Hotel> UpdateOneHotelById(int id, Hotel hotel)
        {
            throw new NotImplementedException();
        }
    }
}
