using AutoMapper;
using ChatJobsity.Chat.Domain.Services;
using ChatJobsity.Chat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : BaseController
    {
        public RoomController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("own/{userId}")]
        public async Task<List<RoomModel>> GetOwnRooms(Guid userId)
        {
            var rooms = await _unitOfWork.Rooms.GetOwnRooms(userId);
            var mapped = _mapper.Map<List<RoomModel>>(rooms);
            return mapped;
        }

        [HttpPost("getorcreate")]
        public async Task<RoomModel> GetOrCreateRoom(Guid fromUserId, Guid toUserId)
        {
            var room = await _unitOfWork.Rooms.GetOrCreateRoom(fromUserId, toUserId);
            room.Participants.Remove(room.Participants.FirstOrDefault(x => x.User.Id == fromUserId));
            return _mapper.Map<RoomModel>(room);            
        }
        
    }
}
