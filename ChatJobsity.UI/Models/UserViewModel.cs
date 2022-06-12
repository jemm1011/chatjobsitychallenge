using System;
using System.Collections.Generic;

namespace ChatJobsity.UI.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public virtual IList<RoomViewModel> Rooms { get; set; }
    }
}
