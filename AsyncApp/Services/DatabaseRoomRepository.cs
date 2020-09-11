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
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetOneRoomById(long id);

        Task CreateRoom(Room room);
        Task<bool> UpdateOneRoom(Room room);
        Task<Room> DeleteOneRoomById(long id);
    }

    public class DatabaseRoomRepository: IRoomRepository
    {
        private readonly HotelDbContext _context;

        public DatabaseRoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task CreateRoom(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> DeleteOneRoomById(long id)
        {
            var room = await _context.Room.FindAsync(id);

            if (room == null)
            {
                return null;
            }

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Room.ToListAsync();
        }

        public async Task<Room> GetOneRoomById(long id)
        {
            var room = await _context.Room.FindAsync(id);
            return room;
        }

        public async Task<bool> UpdateOneRoom(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoomExists((int)room.Id))
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
        private async Task<bool> RoomExists(int id)
        {
            return await _context.Room.AnyAsync(e => e.Id == id);
        }
    }
}
