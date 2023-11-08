using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Hubs
{
    public interface IHubContextFactory
    {
        public IHubContext<T> CreateHub<T>() where T : Hub;
    }
}
