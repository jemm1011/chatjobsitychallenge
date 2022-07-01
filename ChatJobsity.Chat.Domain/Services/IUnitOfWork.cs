using ChatJobsity.Chat.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Domain.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomRepository Rooms { get; }
        IMessageRepository Messages { get; }
        IParticipantRepository Participants { get; }
        Task<int> SaveChanges();
    }
}
