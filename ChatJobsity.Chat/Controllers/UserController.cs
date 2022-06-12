using AutoMapper;
using ChatJobsity.Chat.Domain.Models;
using ChatJobsity.Chat.Domain.Services;
using ChatJobsity.Chat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetChatUsers()
        {
            var users = await _unitOfWork.Users.GetAll();
            return _mapper.Map<List<UserModel>>(users);
        }

        [HttpPost]
        public ActionResult AddUser(UserModel user)
        {
            _unitOfWork.Users.Add(_mapper.Map<User>(user));
            _unitOfWork.SaveChanges();
            return Ok();
        }
    }
}
