using IndiaLivings_Web_UI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using IndiaLivings_Web_DAL.Helpers;

namespace IndiaLivings_Web_UI.Controllers
{
    public class NotificationController : Hub
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