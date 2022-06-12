using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Models
{
    public class RoomParticipantModel
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }
        public RoomModel Room { get; set; }
    }
}
