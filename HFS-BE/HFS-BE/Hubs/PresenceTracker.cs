namespace HFS_BE.Hubs
{
	public class PresenceTracker
	{
		private static readonly Dictionary<string, List<string>> OnlineUsers = new Dictionary<string, List<string>>();
		public Task<bool> UserConnected(string email, string connectionId)
		{
			bool isOnline = false;
			lock (OnlineUsers)
			{
				if (OnlineUsers.ContainsKey(email))
				{
					OnlineUsers[email].Add(connectionId);
				}
				else
				{
					OnlineUsers.Add(email, new List<string> { connectionId });
					isOnline = true;
				}
			}

			return Task.FromResult(isOnline);
		}

		public Task<bool> UserDisconnected(string email, string connectionId)
		{
			bool isOffline = false;
			lock (OnlineUsers)
			{
				if (!OnlineUsers.ContainsKey(email)) return Task.FromResult(isOffline);

				OnlineUsers[email].Remove(connectionId);
				if (OnlineUsers[email].Count == 0)
				{
					OnlineUsers.Remove(email);
					isOffline = true;
				}
			}

			return Task.FromResult(isOffline);
		}

		public Task<string[]> GetOnlineUsers()
		{
			string[] onlineUsers;
			lock (OnlineUsers)
			{
				onlineUsers = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
			}

			return Task.FromResult(onlineUsers);
		}

		public Task<List<string>> GetConnectionsForUser(string email)
		{
			List<string> connectionIds;
			lock (OnlineUsers)
			{
				connectionIds = OnlineUsers.GetValueOrDefault(email);
			}

			return Task.FromResult(connectionIds);
		}
	}

}
