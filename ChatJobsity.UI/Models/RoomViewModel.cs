using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatJobsity.UI.Models
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public string GuestUserDisplayName { get; set; }
        public Guid GuestUserId { get; set; }
        public virtual IList<MessageViewModel> Messages { get; set; }
            
    }
}
