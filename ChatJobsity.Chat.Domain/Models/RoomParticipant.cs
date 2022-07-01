using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Domain.Models
{
    public class RoomParticipant : BaseEntity
    {
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public virtual Guid UserId { get; set; }
    }
}
