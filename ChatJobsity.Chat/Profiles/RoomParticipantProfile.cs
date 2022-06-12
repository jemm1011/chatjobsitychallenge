using AutoMapper;
using ChatJobsity.Chat.Domain.Models;
using ChatJobsity.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Profiles
{
    public class RoomParticipantProfile: Profile
    {
        public RoomParticipantProfile()
        {
            CreateMap<RoomParticipantModel, RoomParticipant>();

            CreateMap<RoomParticipant, RoomParticipantModel>();
        }

    }
}
