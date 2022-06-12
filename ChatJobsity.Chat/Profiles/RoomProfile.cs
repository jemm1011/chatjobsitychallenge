using AutoMapper;
using ChatJobsity.Chat.Domain.Models;
using ChatJobsity.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomModel, Room>()
                .ForMember(dest => dest.Participants, act => act.Ignore());

            CreateMap<Room, RoomModel>()
                .ForMember(dest => dest.GuestUserDisplayName, act => act.MapFrom(src => src.Participants.First().User.DisplayName))
                .ForMember(dest => dest.GuestUserId, act => act.MapFrom(src => src.Participants.First().User.Id));
        }
    }
}
