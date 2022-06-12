using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Domain.Models
{
    public class User : BaseEntity
    {
        public string DisplayName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public virtual ICollection<RoomParticipant> Rooms { get; set; }
    }
}
