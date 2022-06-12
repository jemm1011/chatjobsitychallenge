using System;
using System.Collections.Generic;

namespace ChatJobsity.Chat.Models
{
    public class RoomModel
    {
        public Guid Id { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string GuestUserDisplayName { get; set; }
        public Guid GuestUserId { get; set; }        
        public List<MessageModel> Messages { get; set; }
    }
}
