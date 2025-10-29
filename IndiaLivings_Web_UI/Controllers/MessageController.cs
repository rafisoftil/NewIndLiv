using IndiaLivings_Web_UI.Controllers;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IndiaLivings_Web_UI.Hubs
{
    public class MessageController : Hub
    {
        private readonly IHubContext<NotificationHub> _notificationHub;

        public MessageController(IHubContext<NotificationHub> notificationHub)
        {
            _notificationHub = notificationHub;
        }
        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            await Clients.User(senderId).SendAsync("ReceiveMessage", senderId, receiverId, message);
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, receiverId, message);
            await Clients.User(receiverId).SendAsync("UpdateUnreadCount", receiverId);

            await _notificationHub.Clients.User(receiverId).SendAsync("ReceiveNotification", new
            {
                type = "message",
                message = message,
                data = new { senderId, message },
                timestamp = System.DateTime.Now
            });
        }

        public override Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier ?? Context.ConnectionId;
            return base.OnConnectedAsync();
        }
    }
}
