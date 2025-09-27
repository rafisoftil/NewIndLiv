using Microsoft.AspNetCore.SignalR;
namespace IndiaLivings_Web_UI.Hubs
{
    public class NotificationHub: Hub
    {
        public async Task SendNotification(string userId, string message, string type, string data)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", new
            {
                message = message,
                type = type,
                data = data,
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
