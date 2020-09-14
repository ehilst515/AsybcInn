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
        Task<bool> UpdateOneHotel(Hotel hotel);
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
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> DeleteOneHotelById(long id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return null;
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetOneHotelById(long id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            return hotel;
        }

        public async Task<bool> UpdateOneHotel(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists((int)hotel.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
        private async Task<bool> HotelExists(int id)
        {
            return await _context.Hotels.AnyAsync(e => e.Id == id);
        }
    }
}
