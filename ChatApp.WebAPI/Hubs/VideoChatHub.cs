using Microsoft.AspNetCore.SignalR;

namespace ChatApp.API.Hubs
{
    public class VideoChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.GetHttpContext()!.Request.Query["userId"]);

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.GetHttpContext()!.Request.Query["userId"]);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task Call(string callingUserId, string userId, string peerId)
        {
            await Clients.Group(userId).SendAsync("IncomingCall", callingUserId, peerId);
        }

        public async Task HangUp(string userId)
        {
            await Clients.Group(userId).SendAsync("CallTerminated");
        }

        public async Task DeclineCall(string userId)
        {
            await Clients.Group(userId).SendAsync("CallDeclined");
        }
    }
}
