using ChatJobsity.Chat.Domain.Repositories;
using ChatJobsity.Chat.Domain.Services;
using ChatJobsity.Chat.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Application.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public IRoomRepository Rooms { get; private set; }
        public IMessageRepository Messages { get; private set; }
        public IParticipantRepository Participants { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext, IRoomRepository rooms, IMessageRepository messages, IParticipantRepository participantRepository)
        {
            this.dbContext = dbContext;
            Rooms = rooms;
            Messages = messages;
            Participants = participantRepository;
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
