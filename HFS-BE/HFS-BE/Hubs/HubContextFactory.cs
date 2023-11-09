using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Hubs
{
    public class HubContextFactory : IHubContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public HubContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IHubContext<T> CreateHub<T>() where T : Hub
        {
            var hubContextType = typeof(IHubContext<>).MakeGenericType(typeof(T));
            return (IHubContext<T>)_serviceProvider.GetService(hubContextType);
        }
    }
}
