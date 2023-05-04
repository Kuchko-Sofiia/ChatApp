using ChatApp.DAL.Entities;
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

        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.GetHttpContext()!.Request.Query["roomId"]);
            await Clients.OthersInGroup(roomId).SendAsync("PeerJoined", Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.OthersInGroup(roomId).SendAsync("PeerLeft", Context.ConnectionId);
        }

        public async Task Call(string userId, string peerId)
        {
            await Clients.Group(userId).SendAsync("IncomingCall", userId, peerId);
        }
    }
}
