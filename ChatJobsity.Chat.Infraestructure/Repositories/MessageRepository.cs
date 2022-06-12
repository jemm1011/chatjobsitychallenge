using ChatJobsity.Chat.Domain.Models;
using ChatJobsity.Chat.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Infraestructure.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Message>> GetMessagesByRoomId(Guid roomId)
        {
            throw new NotImplementedException();
        }

        public Task ReadMessages(DateTime lastEntryDate)
        {
            throw new NotImplementedException();
        }
    }
}
