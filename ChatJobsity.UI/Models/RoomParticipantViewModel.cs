﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.UI.Models
{
    public class RoomParticipantViewModel
    {
        public Guid UserId { get; set; }
        public RoomViewModel Room { get; set; }
    }
}
