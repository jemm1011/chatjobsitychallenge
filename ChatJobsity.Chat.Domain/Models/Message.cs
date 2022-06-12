using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJobsity.Chat.Domain.Models
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }
        [ForeignKey("SenderUserId")]
        public virtual User SenderUser { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
