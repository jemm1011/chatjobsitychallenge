using ChatJobsity.Chat.Domain.Models;
using ChatJobsity.Chat.Domain.Repositories;

namespace ChatJobsity.Chat.Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
