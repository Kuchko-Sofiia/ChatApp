using Microsoft.AspNetCore.SignalR;
using System.Security.Policy;

namespace ChatApp.API.Hubs
{
    public class VideoChatHub : Hub
    {
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

        public async Task CallUser(string userId)
        {
            //TODO: implement logic of notifying another user
        }
    }
}
