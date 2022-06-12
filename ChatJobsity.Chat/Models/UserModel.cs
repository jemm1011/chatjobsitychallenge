using System;
using System.Collections.Generic;

namespace ChatJobsity.Chat.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public List<RoomModel> Rooms { get; set; }
    }
}
