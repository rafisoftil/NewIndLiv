using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IndiaLivings_Web_UI.Hubs
{
    public class Message : Hub
    {
        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            // Broadcast to specific users (both sender and receiver)
            await Clients.User(senderId).SendAsync("ReceiveMessage", senderId, receiverId, message);
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, receiverId, message);
        }

        public override Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier ?? Context.ConnectionId;
            return base.OnConnectedAsync();
        }
    }
}
