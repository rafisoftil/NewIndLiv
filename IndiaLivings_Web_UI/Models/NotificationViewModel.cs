using IndiaLivings_Web_DAL.Helpers;
using IndiaLivings_Web_DAL.Models;
using System.Globalization;

namespace IndiaLivings_Web_UI.Models
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserMiddleName { get; set; }
        public string UserLastName { get; set; }
        public byte[] UserImageData { get; set; }
        public int SenderUserId { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastMessageTime { get; set; }
        public int IsRead { get; set; }
        public int UnreadCount { get; set; }
        public string NotificationType { get; set; }
        public int ProductId { get; set; }

        public string TimeAgo
        {
            get
            {
                var now = DateTime.Now;
                var timeDiff = now - LastMessageTime;

                if (timeDiff.TotalSeconds < 60)
                    return "just now";
                if (timeDiff.TotalMinutes < 60)
                {
                    int mins = (int)timeDiff.TotalMinutes;
                    return mins == 1 ? "1 min ago" : $"{mins} min ago";
                }
                if (timeDiff.TotalHours < 24)
                {
                    int hours = (int)timeDiff.TotalHours;
                    return hours == 1 ? "1 hour ago" : $"{hours} hours ago";
                }
                if (timeDiff.TotalDays < 7)
                {
                    int days = (int)timeDiff.TotalDays;
                    return days == 1 ? "1 day ago" : $"{days} days ago";
                }
                if (timeDiff.TotalDays < 30)
                {
                    int weeks = (int)(timeDiff.TotalDays / 7);
                    return weeks == 1 ? "1 week ago" : $"{weeks} weeks ago";
                }
                if (timeDiff.TotalDays < 365)
                {
                    int months = (int)(timeDiff.TotalDays / 30);
                    return months == 1 ? "1 month ago" : $"{months} months ago";
                }
                int years = (int)(timeDiff.TotalDays / 365);
                return years == 1 ? "1 year ago" : $"{years} years ago";
            }
        }

        public List<NotificationViewModel> GetUserNotifications(int userId)
        {
            AuthenticationHelper AH = new AuthenticationHelper();
            List<NotificationViewModel> NVM = new List<NotificationViewModel>();
            try
            {
                var notifications = AH.GetNotificationsByUser(userId);
                foreach (var note in notifications)
                {
                    NotificationViewModel nvm = new NotificationViewModel
                    {
                        NotificationId = note.NotificationId,
                        UserId = note.UserId,
                        UserName = note.UserName,
                        UserFirstName = note.UserFirstName,
                        UserMiddleName = note.UserMiddleName,
                        UserLastName = note.UserLastName,
                        UserImageData = note.UserImageData,
                        SenderUserId = note.SenderUserId,
                        LastMessage = note.LastMessage,
                        LastMessageTime = note.LastMessageTime,
                        IsRead = note.IsRead,
                        UnreadCount = note.UnreadCount,
                        NotificationType = note.NotificationType,
                        ProductId = note.ProductId
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
        public string MarkProductNotificationAsRead(int notificationId, int userId)
        {
            ProductHelper PH = new ProductHelper();
            string result = string.Empty;
            try
            {
                result = PH.MarkProductNotificationAsRead(notificationId, userId);
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            return result;
        }
    }
}
