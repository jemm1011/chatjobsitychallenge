using ChatJobsity.Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Domain.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<List<Room>> GetOwnRooms(Guid userId);
        Task<List<Room>> GetAvailableRooms(Guid userId);
        Task<Room> CreateRoom(Guid userId, string roomName);
    }
}
