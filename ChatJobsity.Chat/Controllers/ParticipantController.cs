using AutoMapper;
using ChatJobsity.Chat.Domain.Models;
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
    public class ParticipantController : BaseController
    {
        public ParticipantController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("{roomId}")]
        public async Task<List<RoomParticipantModel>> GetParticipantsByRoomId(Guid roomId)
        {
            var participants = await _unitOfWork.Participants.GetParticipantsByRoomId(roomId);
            var mapped = _mapper.Map<List<RoomParticipantModel>>(participants);
            return mapped;
        }

        [HttpPost()]
        public async Task<IActionResult> AddRoomParticipant(RoomParticipantModel roomParticipant)
        {
            var entity = _mapper.Map<RoomParticipant>(roomParticipant);
            entity.Room = await _unitOfWork.Rooms.GetById(roomParticipant.Room.Id);
            await _unitOfWork.Participants.Add(entity);
            await _unitOfWork.SaveChanges();
            return Ok();
        }
    }
}
