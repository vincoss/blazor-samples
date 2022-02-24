using Microsoft.AspNetCore.SignalR;

namespace Tutorial_BlazorServerSignalRAppServer.Hubs;


public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
