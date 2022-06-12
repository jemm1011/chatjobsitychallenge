using ChatJobsity.Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Domain.Repositories
{
    public interface IParticipantRepository : IRepository<RoomParticipant>
    {
        Task<List<RoomParticipant>> GetParticipantsByRoomId(Guid roomId);
    }
}
