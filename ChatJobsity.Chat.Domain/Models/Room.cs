using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Domain.Models
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Guid CreatorUserId { get; set; }
        public virtual ICollection<RoomParticipant> Participants { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
