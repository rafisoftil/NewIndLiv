using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;

namespace IndiaLivings_Web_UI.Models
{
    public class NotificationViewModel
    {
        public int SenderUserId { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
        public int UnreadCount { get; set; }

        public List<NotificationViewModel> GetUserNotifications(int userId)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<NotificationViewModel> NVM = new List<NotificationViewModel>();
            try
            {
                var notifications = AH.GetNotificationsByUser(userId);
                foreach (var not in notifications)
                {
                    NotificationViewModel nvm = new NotificationViewModel()
                    {
                        SenderUserId = not.SenderUserId,
                        LastMessage = not.LastMessage,
                        LastMessageTime = not.LastMessageTime,
                        UnreadCount = not.UnreadCount
                    };
                    NVM.Add(nvm);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return NVM;
        }
    }
}
