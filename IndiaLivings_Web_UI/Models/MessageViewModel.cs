using IndiaLivings_Web_DAL;
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
        public bool IsRead { get; set; }
        public DateTime Timestamp { get; set; }

        public List<MessageViewModel> GetMessageByUser(int SenderUserId, int ReceiverUserId)
        {
            List<MessageViewModel> messages = new List<MessageViewModel>();
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                var messageList = AH.GetMessagesByUserId(SenderUserId, ReceiverUserId);
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
                            IsRead = message.isRead,
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
        public string DeleteUserMessage(int messageId, int userId)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            string response = "An error occurred";
            try
            {
                response = AH.DeleteUserMessage(messageId, userId);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public string MarkMessagesAsRead(int messageId, int receiverUserId)
        {
            string response = "An error occured";
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                response = AH.MarkMessagesAsRead(messageId, receiverUserId);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return response;
        }
        public int GetUnreadMessageCount(int receiverUserId)
        {
            int unreadCount = 0;
            AuthenticationHelper AH = new AuthenticationHelper();
            try
            {
                unreadCount = AH.GetUnreadMessageCount(receiverUserId);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return unreadCount;
        }
    }
}
