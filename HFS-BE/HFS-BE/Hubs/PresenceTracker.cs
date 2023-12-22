namespace HFS_BE.Hubs
{
	public class PresenceTracker
	{
		private static readonly Dictionary<string, List<string>> OnlineUsers = new Dictionary<string, List<string>>();
		private static readonly Dictionary<string, List<string>> CustomerOnline = new Dictionary<string, List<string>>();
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
		public Task<bool> CustomerConnected(string email, string connectionId)
		{
			bool isOnline = false;
			lock (CustomerOnline)
			{
				if (CustomerOnline.ContainsKey(email))
				{
					CustomerOnline[email].Add(connectionId);
				}
				else
				{
					CustomerOnline.Add(email, new List<string> { connectionId });
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
		public Task<bool> CustomerDisconnected(string email, string connectionId)
		{
			bool isOffline = false;
			lock (CustomerOnline)
			{
				if (!CustomerOnline.ContainsKey(email)) return Task.FromResult(isOffline);

				CustomerOnline[email].Remove(connectionId);
				if (CustomerOnline[email].Count == 0)
				{
					CustomerOnline.Remove(email);
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

		public Task<string[]> GetOnlineCustomer()
		{
			string[] onlineUsers;
			lock (CustomerOnline)
			{
				onlineUsers = CustomerOnline.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
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
		public Task<List<string>> GetConnectionsForCustomer(string email)
		{
			List<string> connectionIds;
			lock (CustomerOnline)
			{
				connectionIds = CustomerOnline.GetValueOrDefault(email);
			}

			return Task.FromResult(connectionIds);
		}
	}

}
