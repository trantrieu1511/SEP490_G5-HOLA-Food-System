using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Hubs
{
    [Authorize]
    public class DataRealTimeHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = TokenQuerySignalR.GetSingleton().GetUserId(Context);
            if (userId == null)
            {
                throw new HubException("Connection failed: Unauthorized");
            }
            Groups.AddToGroupAsync(Context.ConnectionId, userId);
            return base.OnConnectedAsync();
        }
    }
}
