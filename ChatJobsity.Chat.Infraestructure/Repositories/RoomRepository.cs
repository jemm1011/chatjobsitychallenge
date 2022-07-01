using ChatJobsity.Chat.Domain.Models;
using ChatJobsity.Chat.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Infraestructure.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Room> CreateRoom(Guid userId, string roomName)
        {
            var room = new Room()
            {
                Id = Guid.NewGuid(),
                Name = roomName,
                CreatorUserId = userId,
                CreatedDateTime = DateTime.Now,
                LastUpdatedDateTime = DateTime.Now
            };
            await _context.Rooms.AddAsync(room);
            var participants = new List<RoomParticipant>
            {
                { new RoomParticipant()
                    {
                        Id = Guid.NewGuid(),
                        Room = room,
                        UserId = userId
                    }
                }
            };
            await _context.Participants.AddRangeAsync(participants);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<List<Room>> GetOwnRooms(Guid userId)
        {
            return await _context.Rooms
                .Include(x => x.Participants.Where(x => x.UserId != userId))
                .Include(x => x.Messages.OrderBy(x => x.SentDateTime).Take(50))
                .Where(x => x.Participants.Any(y => y.UserId == userId))
                .OrderBy(x => x.LastUpdatedDateTime)
                .ToListAsync();
                
        }

        public async Task<List<Room>> GetAvailableRooms(Guid userId)
        {
            return await _context.Rooms
                .Where(x => !x.Participants.Any(y => y.UserId == userId))
                .OrderBy(x => x.LastUpdatedDateTime)
                .ToListAsync();

        }
    }
}
