using Microsoft.AspNetCore.SignalR;
using ChatApp.DTO;

namespace ChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendNewMessageAsync(MessageDTO _currentMessage)
        {
            await Clients.All.SendAsync("ReceiveMessage", _currentMessage);
        }
    }
}
