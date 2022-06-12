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
    public class MessageController : BaseController
    {
        public MessageController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(MessageModel message)
        {
            var mapped = _mapper.Map<Message>(message);
            mapped.Room = await _unitOfWork.Rooms.GetById(message.RoomId);
            mapped.SenderUser = await _unitOfWork.Users.GetById(message.SenderUserId);
            mapped.SentDateTime = DateTime.Now;
            await _unitOfWork.Messages.Add(mapped);

            mapped.Room.LastUpdatedDateTime = DateTime.Now;
            await _unitOfWork.Rooms.Update(mapped.Room);
            return Ok();
        }

    }
}
