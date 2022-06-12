using ChatJobsity.UI.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatJobsity.UI.Apis
{
    public interface IChatApi
    {
        #region UserApi
        [Get("/user")]
        Task<List<UserViewModel>> GetChatUsers();

        [Post("/user")]
        Task AddUser(UserViewModel user);
        #endregion

        #region RoomApi
        [Get("/room/own/{userId}")]
        Task<List<RoomViewModel>> GetOwnRooms(Guid userId);

        [Post("/room/getorcreate")]
        Task<RoomViewModel> GetOrCreateRoom(Guid fromUserId, Guid toUserId);
        #endregion

        #region MessageApi
        [Post("/message")]
        Task SendMessage(MessageViewModel message);
        #endregion

        #region ParticipantApi
        [Get("/participant/{roomId}")]
        Task<List<RoomParticipantViewModel>> GetParticipantsByRoomId(Guid roomId);
        #endregion
    }
}