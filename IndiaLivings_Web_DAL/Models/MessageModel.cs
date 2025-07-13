using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaLivings_Web_DAL.Models
{
    public class MessageModel
    {
        public int messageId { get; set; }
        public string senderUserId { get; set; }
        public string receiverUserId { get; set; }
        public string messageText { get; set; }
        public DateTime sentAt { get; set; }
        public bool isRead { get; set; }
    }
}