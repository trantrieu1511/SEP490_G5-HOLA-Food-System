using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Hubs
{

    [Authorize]
    public class DataRealTimeHub : Hub
    {
        private Dictionary<string, string> UserConnections = new Dictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            var userId = TokenQuerySignalR.GetSingleton().GetUserId(Context);
            if (userId == null)
            {
                throw new HubException("Connection failed: Unauthorized");
            }
            var connectionId = Context.ConnectionId;
/*
            // Thêm shipper và thông tin ConnectionId vào dictionary
            if (!UserConnections.ContainsKey(userId))
            {
                UserConnections.Add(userId, connectionId);
            }
            else
            {
                UserConnections[userId] = connectionId;
            }*/

            // Thêm user vào nhóm
            Groups.AddToGroupAsync(connectionId, userId);

            
            return base.OnConnectedAsync();
        }

        public async Task SendNotificationToUser(string userId, string message)
        {
        }
    }
}
