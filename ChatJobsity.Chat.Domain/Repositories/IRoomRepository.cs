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
        Task<Room> GetOrCreateRoom(Guid fromUserId, Guid toUserId);
    }
}
