using Microsoft.AspNetCore.SignalR;
using ChatApp.DTO;
using System.Security.Claims;

namespace ChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendNewMessageAsync(int chatId, MessageDTO message)
        {
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.GetHttpContext()!.Request.Query["chatId"]);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.GetHttpContext()!.Request.Query["chatId"]);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
