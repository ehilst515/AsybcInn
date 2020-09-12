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
        IEnumerable<Room> GetAllAsync();
        Room GetOneRoomById(int id);

        Task CreateRoom(Room room);
        Task<bool> UpdateOneRoom(Room room);
        Task<Room> DeleteOneRoomById(int id);

        Task AddAmenityToRoom(int roomId, long amenityId);
        Task DeleteAmenityFromRoom(int roomId, long amenityId);
    }

    public class DatabaseRoomRepository: IRoomRepository
    {
        private readonly HotelDbContext _context;


        public DatabaseRoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task AddAmenityToRoom(int roomId, long amenityId)
        {
            var roomAmenity = new RoomAmenity
            {
                AmenityId = amenityId,
                RoomId = roomId,
            };
            _context.RoomAmenities.Add(roomAmenity);
            await _context.SaveChangesAsync();
        }
    

        public async Task CreateRoom(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAmenityFromRoom(int roomId, long amenityId)
        {
            var roomAmenity = await _context.RoomAmenities.FindAsync(roomId, amenityId);
            _context.RoomAmenities.Remove(roomAmenity);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> DeleteOneRoomById(int id)
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

        public  IEnumerable<Room> GetAllAsync()
        {
            //return await _context.Room.ToListAsync();
            return _context.Room
                .Include(r => r.RoomAmenities)
                .ToList();
        }

        public Room GetOneRoomById(int id)
        {
            //var room = await _context.Room.FindAsync(id);
            //return room;

            return _context.Room
                .Include(r => r.RoomAmenities)
                .FirstOrDefault(r => r.Id == id);
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
