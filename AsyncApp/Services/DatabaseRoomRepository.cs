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
        Task<IEnumerable<Room>>GetAllAsync();
        Task<Room> GetOneRoomByIdAsync(long id);

        Task CreateRoom(Room room);
        Task<bool> UpdateOneRoom(Room room);
        Task<Room> DeleteOneRoomById(long id);

        Task AddAmenityToRoom(long roomId, long amenityId);
        Task DeleteAmenityFromRoom(long roomId, long amenityId);

        Task AddRoomAmenity(long AmenityId, long RoomId);
    }

    public class DatabaseRoomRepository: IRoomRepository
    {
        private readonly HotelDbContext _context;


        public DatabaseRoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task AddAmenityToRoom(long roomId, long amenityId)
        {
            var roomAmenity = new RoomAmenity
            {
                AmenityId = amenityId,
                RoomId = roomId,
            };

            _context.RoomAmenities.Add(roomAmenity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRoomAmenity(long amenityId, long roomId)
        {
            var roomAmenity = new RoomAmenity
            {
                AmenityId = amenityId,
                RoomId = roomId
            };

            _context.RoomAmenities.Add(roomAmenity);
            await _context.SaveChangesAsync();

        }

        public async Task CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAmenityFromRoom(long roomId, long amenityId)
        {
            var roomAmenity = await _context.RoomAmenities.FindAsync(roomId, amenityId);
            _context.RoomAmenities.Remove(roomAmenity);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> DeleteOneRoomById(long id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return null;
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms
                .Include(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenity)
                .ToListAsync();
        }

        public async Task<Room> GetOneRoomByIdAsync(long id)
        {
              var room = await _context.Rooms.FindAsync(id);
          return room;

            //return _context.Rooms
            //    .Include(r => r.RoomAmenities)
            //    .FirstOrDefault(r => r.Id == id);
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
                if (!await RoomExists(room.Id))
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
        private async Task<bool> RoomExists(long id)
        {
            return await _context.Rooms.AnyAsync(e => e.Id == id);
        }
    }
}
