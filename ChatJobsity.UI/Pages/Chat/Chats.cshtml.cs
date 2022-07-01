using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatJobsity.Shared.Models;
using ChatJobsity.UI.Apis;
using ChatJobsity.UI.Hubs;
using ChatJobsity.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChatJobsity.UI.Pages.Chat
{
    public class ChatsModel : PageModel
    {
        public List<RoomViewModel> Rooms { get; set; }

        public RoomViewModel SelectedRoom { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TextMessage { get; set; }

        private readonly IChatApi _chatApi;
        private readonly IBotApi _botApi;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        private const string ROOMS_KEY_PREFIX = "RSK";
        private const string SELC_KEY_PREFIX = "SERSK";

        public ChatsModel(IChatApi chatApi, IBotApi botApi,
            UserManager<IdentityUser> userManager, IHubContext<ChatHub> hubContext)
        {
            _chatApi = chatApi;
            _botApi = botApi;
            _userManager = userManager;
            _hubContext = hubContext;
        }
        public async Task OnGet()
        {
            Rooms = await _chatApi.GetOwnRooms(new Guid(_userManager.GetUserId(User)));
            HttpContext.Session.SetString($"{ROOMS_KEY_PREFIX}-{_userManager.GetUserId(User)}", JsonConvert.SerializeObject(Rooms));
        }

        public async Task<IActionResult> OnPostSelect(Guid roomId)
        {
            await LoadRoomData(roomId, true);
            return Page();
        }

        public async Task<IActionResult> OnPostSend(Guid roomId)
        {
            if (TextMessage.ToLower().Contains("/stock="))
            {
                await ProcessQuoteMessage();
            }
            else
            {
                var newMessage = new MessageViewModel
                {
                    Id = Guid.NewGuid(),
                    SenderUserId = new Guid(_userManager.GetUserId(User)),
                    RoomId = roomId,
                    Text = TextMessage,
                    SentDateTime = DateTime.Now
                };
                await _chatApi.SendMessage(newMessage);
                var fetchFromServer = await LoadRoomData(roomId, false);
                if (!fetchFromServer)
                {
                    Rooms.FirstOrDefault(x => x.Id == newMessage.RoomId).Messages.Add(newMessage);
                    HttpContext.Session.SetString($"{ROOMS_KEY_PREFIX}-{_userManager.GetUserId(User)}", JsonConvert.SerializeObject(Rooms));
                }
                await NotifyAllParticipants(roomId);
            }            
            TextMessage = string.Empty;
            return Page();
        }

        private async Task ProcessQuoteMessage()
        {
            var stockCode = TextMessage.Substring(TextMessage.IndexOf("=") + 1);
            await _botApi.ProcessQuote(new QuoteRequest()
            {
                QuoteCode = stockCode,
                UserId = new Guid(_userManager.GetUserId(User))
            });

        }

        public async Task<IActionResult> OnPostRefresh(Guid roomId, string message, bool isBot)
        {            
            
            if (isBot)
            {
                await LoadRoomData(roomId, false);
                var newMessage = new MessageViewModel
                {
                    Id = Guid.Empty,
                    SenderUserId = Guid.Empty,
                    SenderUserName =  _userManager.GetUserName(User),
                    RoomId = roomId,
                    Text = message,
                    SentDateTime = DateTime.Now
                };
                SelectedRoom.Messages.Add(newMessage);
            }
            else
            {
                await LoadRoomData(roomId, true);
            }

            return Page();
        }

        private async Task NotifyAllParticipants(Guid roomId)
        {
            var participants = await _chatApi.GetParticipantsByRoomId(roomId);
            var currentUserId = new Guid(_userManager.GetUserId(User));
            foreach (var participant in participants)
            {
                if (participant.UserId != currentUserId)
                {
                    await _hubContext.Clients.User(participant.UserId.ToString()).SendAsync("Receive", "New Message", false);
                }
            }
        }

        private async Task<bool> LoadRoomData(Guid roomId, bool forceRefresh)
        {
            var fetchFromServer = true;
            if (!forceRefresh)
            {
                var roomsSerialized = HttpContext.Session.GetString($"{ROOMS_KEY_PREFIX}-{_userManager.GetUserId(User)}");
                if (!string.IsNullOrEmpty(roomsSerialized))
                {
                    Rooms = JsonConvert.DeserializeObject<List<RoomViewModel>>(roomsSerialized);
                    fetchFromServer = false;
                }
            }
            if(Rooms == null)
            {
                Rooms = await _chatApi.GetOwnRooms(new Guid(_userManager.GetUserId(User)));
                HttpContext.Session.SetString($"{ROOMS_KEY_PREFIX}-{_userManager.GetUserId(User)}", JsonConvert.SerializeObject(Rooms));
            }
            SelectedRoom = Rooms.FirstOrDefault(x => x.Id == roomId);
            foreach (var message in SelectedRoom.Messages)
            {
                message.SenderUserName = (await _userManager.FindByIdAsync(message.SenderUserId.ToString())).UserName;
            }
            return fetchFromServer;
        }
    }
}
