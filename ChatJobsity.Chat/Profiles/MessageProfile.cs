using AutoMapper;
using ChatJobsity.Chat.Domain.Models;
using ChatJobsity.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageModel>()
                .ForMember(dest => dest.SenderUserId, act => act.MapFrom(src => src.SenderUser.Id))
                .ForMember(dest => dest.RoomId, act => act.MapFrom(src => src.Room.Id));

            CreateMap<MessageModel, Message>();
        }
    }
}
