using ChatJobsity.Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Domain.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetMessagesByRoomId(Guid roomId);
        Task ReadMessages(DateTime lastEntryDate);
    }
}
