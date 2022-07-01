using ChatJobsity.UI.Apis;
using ChatJobsity.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJobsity.UI.Views.Chat
{
    public class RoomsModel : PageModel
    {
        private readonly IChatApi _chatApi;
        private readonly UserManager<IdentityUser> _userManager;

        public List<RoomViewModel> AvailableRooms { get; set; }

        [BindProperty]
        public string RoomName { get; set; }

        public RoomsModel(IChatApi chatApi, UserManager<IdentityUser> userManager)
        {
            _chatApi = chatApi;
            _userManager = userManager;
        }

        public async Task OnGet()
        {
            await LoadRooms();
        }

        public async Task<IActionResult> OnPostCreate()
        {
            var currentUserId = _userManager.GetUserId(User);
            var room = await _chatApi.CreateRoom(new Guid(currentUserId), RoomName);
            return Redirect("/Chat/Chats");
        }

        public async Task<IActionResult> OnPostEnter(Guid roomId)
        {
            var currentUserId = _userManager.GetUserId(User);
            var participant = new RoomParticipantViewModel()
            {
                Room = new RoomViewModel
                {
                    Id = roomId
                },
                UserId = new Guid(currentUserId)
            };

            await _chatApi.AddRoomParticipant(participant);
            return Redirect("/Chat/Chats");
        }

        private async Task LoadRooms()
        {
            var currentUserId = new Guid(_userManager.GetUserId(User));
            AvailableRooms = await _chatApi.GetAvailableRooms(currentUserId);
        }
    }
}
