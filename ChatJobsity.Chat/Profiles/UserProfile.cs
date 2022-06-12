using AutoMapper;
using ChatJobsity.Chat.Domain.Models;
using ChatJobsity.Chat.Models;
using System;

namespace ChatJobsity.Chat.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>().PreserveReferences()
                .ForMember(dest => dest.Rooms, act => act.Ignore());

            CreateMap<UserModel, User>()
                .ForMember(dest => dest.CreatedDateTime, act => act.MapFrom(src => DateTime.Now));
        }
    }
}
