using ChatJobsity.UI.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatJobsity.UI.Apis
{
    public interface IChatApi
    {
        #region RoomApi
        [Get("/room/own/{userId}")]
        Task<List<RoomViewModel>> GetOwnRooms(Guid userId);
        [Get("/room/available/{userId}")]
        Task<List<RoomViewModel>> GetAvailableRooms(Guid userId);

        [Post("/room/create")]
        Task<RoomViewModel> CreateRoom(Guid userId, string roomName);
        #endregion

        #region MessageApi
        [Post("/message")]
        Task SendMessage(MessageViewModel message);
        #endregion

        #region ParticipantApi
        [Get("/participant/{roomId}")]
        Task<List<RoomParticipantViewModel>> GetParticipantsByRoomId(Guid roomId);

        [Post("/participant")]
        Task AddRoomParticipant(RoomParticipantViewModel roomParticipant);
        #endregion
    }
}