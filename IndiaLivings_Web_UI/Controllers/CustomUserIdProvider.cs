using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

public class CustomUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        // Fetch from session or claims — fallback to connectionId
        var userId = connection.GetHttpContext()?.Session.GetInt32("UserId");
        return userId?.ToString() ?? connection.ConnectionId;
    }
}
