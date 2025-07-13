using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string text { get; set; }
        public DateTime Timestamp { get; set; }

        public List<MessageViewModel> GetMessageByUser(int userId)
        {
            List<MessageViewModel> messages = new List<MessageViewModel>();
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                var messageList = AH.GetMessagesByUserId(userId);
                if (messageList != null)
                {
                    foreach (var message in messageList)
                    {
                        MessageViewModel messageView = new MessageViewModel
                        {
                            Id = message.messageId,
                            SenderId = message.senderUserId,
                            ReceiverId = message.receiverUserId,
                            text = message.messageText,
                            Timestamp = message.sentAt
                        };
                        messages.Add(messageView);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return messages;
        }
    }
}
