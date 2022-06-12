using ChatJobsity.UI.Apis;
using ChatJobsity.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatJobsity.UI.Views.Chat
{
    public class UserListModel : PageModel
    {
        private readonly IChatApi _chatApi;
        private readonly UserManager<IdentityUser> _userManager;
        public UserListModel(IChatApi chatApi, UserManager<IdentityUser> userManager)
        {
            _chatApi = chatApi;
            _userManager = userManager;
        }

        public async Task OnPostGo(Guid userId)
        {
            var currentUserId = _userManager.GetUserId(User);
            var room = await _chatApi.GetOrCreateRoom(new Guid(currentUserId), userId);
        }

        public void OnGet()
        {
        }
    }
}
