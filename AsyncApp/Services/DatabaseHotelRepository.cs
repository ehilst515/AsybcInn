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
        Task<Hotel> GetOneHotelById(long id);

        Task CreateHotel(Hotel hotel);
        Task<Hotel> UpdateOneHotelById(long id, Hotel hotel);
        Task<Hotel> DeleteOneHotelById(long id);
    }

    public class DatabaseHotelRepository: IHotelRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseHotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task CreateHotel(Hotel hotel)
        {
            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> DeleteOneHotelById(long id)
        {
            var hotel = await _context.Hotel.FindAsync(id);

            if (hotel == null)
            {
                return null;
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotel.ToListAsync();
        }

        public async Task<Hotel> GetOneHotelById(long id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            return hotel;
        }

        public Task<Hotel> UpdateOneHotelById(long id, Hotel hotel)
        {
            throw new NotImplementedException();
        }

        private bool HotelExists(long id)
        {
            return _context.Hotel.Any(e => e.Id == id);
        }
    }
}
