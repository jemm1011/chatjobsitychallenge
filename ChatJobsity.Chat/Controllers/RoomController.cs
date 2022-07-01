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


        [HttpGet("available/{userId}")]
        public async Task<List<RoomModel>> GetaAvailableRooms(Guid userId)
        {
            var rooms = await _unitOfWork.Rooms.GetAvailableRooms(userId);
            var mapped = _mapper.Map<List<RoomModel>>(rooms);
            return mapped;
        }

        [HttpPost("create")]
        public async Task<RoomModel> CreateRoom(Guid userId, string roomName)
        {
            var room = await _unitOfWork.Rooms.CreateRoom(userId, roomName);
            return _mapper.Map<RoomModel>(room);            
        }
        
    }
}
