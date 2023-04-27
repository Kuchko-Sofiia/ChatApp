using Microsoft.AspNetCore.SignalR;
using ChatApp.DTO;

namespace ChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(MessageDTO message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
        }
    }
}
