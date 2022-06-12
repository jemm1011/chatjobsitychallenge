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

        public async Task<Room> GetOrCreateRoom(Guid fromUserId, Guid toUserId)
        {
            var fromUserRooms = await _context.Participants
                 .Include(x => x.Room)
                 .Include(x => x.User)
                .Where(x => x.User.Id == fromUserId).ToListAsync();
            if (fromUserRooms == null)
            {
                return await CreateRoomAndParticipants(fromUserId, toUserId);
            }
            else
            {
                try
                {
                    var roomExists = (await _context.Participants
                        .Include(x => x.Room)
                        .Include(x => x.User)
                        .Where(x => x.User.Id == toUserId).ToListAsync()).FirstOrDefault(x => fromUserRooms.Any(y => y.Room.Id == x.Room.Id));                
                
                    if (roomExists == null)
                    {
                        return await CreateRoomAndParticipants(fromUserId, toUserId);
                    }
                    return roomExists.Room;
                 }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private async Task<Room> CreateRoomAndParticipants(Guid fromUserId, Guid toUserId)
        {
            var room = new Room()
            {
                Id = Guid.NewGuid(),
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
                        User =  _context.Users.Find(fromUserId)
                    }
                },
                { new RoomParticipant()
                    {
                        Id = Guid.NewGuid(),
                        Room = room,
                        User =  _context.Users.Find(toUserId)
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
                .Include(x => x.Participants.Where(x => x.User.Id != userId))
                .ThenInclude(x => x.User)
                .Include(x => x.Messages.OrderBy(x => x.SentDateTime).Take(50))
                .ThenInclude(x => x.SenderUser)
                .Where(x => x.Participants.Any(y => y.User.Id == userId))
                .OrderBy(x => x.LastUpdatedDateTime)
                .ToListAsync();
                
        }
    }
}
