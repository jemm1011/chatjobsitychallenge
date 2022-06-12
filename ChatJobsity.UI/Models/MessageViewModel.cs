using System;

namespace ChatJobsity.UI.Models
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid SenderUserId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
