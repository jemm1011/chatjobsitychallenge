using ChatJobsity.Chat.Domain.Models;
using ChatJobsity.Chat.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Infraestructure.Repositories
{
    public class ParticipantRepository : BaseRepository<RoomParticipant>, IParticipantRepository
    {
        public ParticipantRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<RoomParticipant>> GetParticipantsByRoomId(Guid roomId)
        {
            return await _context.Participants.Where(x => x.Room.Id == roomId).ToListAsync();
        }
    }
}
