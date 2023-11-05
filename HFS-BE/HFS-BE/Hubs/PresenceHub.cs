using HFS_BE.DAO.SellerDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Hubs.Extensions;
using HFS_BE.Models;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace HFS_BE.Hubs
{

	public class PresenceHub : Hub
	{
		private readonly PresenceTracker _tracker;
		private readonly SellerDao sellerDao;
		private List<SellerDtoOutput> usersOnline = new List<SellerDtoOutput>();
		private List<SellerDtoOutput> usersOffline = new List<SellerDtoOutput>();
		public PresenceHub(PresenceTracker tracker, SellerDao sellerDao)
		{
			_tracker = tracker;
			this.sellerDao = sellerDao;
		}

		public override async Task OnConnectedAsync()
		{
			var username = Context.User.GetEmail();
		
			//var username = Context.User.FindFirstValue(ClaimTypes.Email);
			var isOnline = await _tracker.UserConnected(username, Context.ConnectionId);
			if (isOnline)
			{

				var user = await sellerDao.GetSellersAsync(username);
				await Clients.Others.SendAsync("UserIsOnline", user);



				var currentUsers = await _tracker.GetOnlineUsers();
				usersOnline = await sellerDao.GetUsersOnlineAsync(username, currentUsers);
				usersOffline = await sellerDao.GetUsersOfflineAsync(username, usersOnline);
			
				await Clients.Caller.SendAsync("GetOnlineAndOfflineUsers", usersOnline, usersOffline);
			}
		}
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			var emailClaim = Context.User.FindFirst(c => c.Type == ClaimTypes.Email);
			var username = emailClaim?.Value;
			var isOffline = await _tracker.UserDisconnected(username, Context.ConnectionId);
			if (isOffline)
			{
				var user = await sellerDao.GetSellersAsync(username);
				await Clients.Others.SendAsync("UserIsOffline", user);

				usersOnline.Remove(user); // Xóa người dùng khỏi danh sách usersOnline
				usersOffline.Add(user);
			}

			
			await base.OnDisconnectedAsync(exception);
		}
	}
}
