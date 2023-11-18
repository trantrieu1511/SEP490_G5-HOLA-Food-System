using HFS_BE.DAO.ChatMessageDao;
using HFS_BE.DAO.CustomerDao;
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
		private readonly CustomerDao customerDao;
		private readonly ChatMessageDao messageDao;
		private List<SellerMessageDtoOutput> usersOnline = new List<SellerMessageDtoOutput>();
		private List<SellerMessageDtoOutput> usersOffline = new List<SellerMessageDtoOutput>();
		private List<SellerMessageDtoOutput> usersOnlineCUS = new List<SellerMessageDtoOutput>();
		private List<SellerMessageDtoOutput> usersOfflineCUS = new List<SellerMessageDtoOutput>();
		public PresenceHub(PresenceTracker tracker, SellerDao sellerDao, CustomerDao customerDao, ChatMessageDao messageDao)
		{
			_tracker = tracker;
			this.sellerDao = sellerDao;
			this.customerDao = customerDao;
			this.messageDao = messageDao;
		}

		public override async Task OnConnectedAsync()
		{

			var username = Context.User.GetEmail();
		   var role = Context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
			if (role == "CU")
			{
				var isOnline = await _tracker.CustomerConnected(username, Context.ConnectionId);
				var currentUsers = await _tracker.GetOnlineUsers();
				usersOnlineCUS = await sellerDao.GetUsersOnlineCustomerAsync(currentUsers);
				usersOfflineCUS = await sellerDao.GetUsersOfflineCustomerAsync(usersOnlineCUS);
				foreach(var user in usersOnlineCUS)
				{
					user.CountMessageNotIsRead = await messageDao.CountMessageCustomerNotIsRead(username, user.Email);
				}
				foreach (var user in usersOfflineCUS)
				{
					user.CountMessageNotIsRead = await messageDao.CountMessageCustomerNotIsRead(username, user.Email);
				}
				await Clients.All.SendAsync("GetOnlineAndOfflineUsersCUS", usersOnlineCUS, usersOfflineCUS);
			}
			if (role == "SE")
			{
				var isOnline = await _tracker.UserConnected(username, Context.ConnectionId);
				if (isOnline)
				{

					var user = await sellerDao.GetSellersAsync(username);
					user.CountMessageNotIsRead = await messageDao.CountMessageCustomerNotIsRead(username, user.Email);
					await Clients.All.SendAsync("UserIsOnline", user);
					var listcus = await customerDao.ListCustomersendSellerbySellerAsync(username);
					foreach (var u in listcus)
					{
						u.CountMessageNotIsRead = await messageDao.CountMessageSellerNotIsRead(u.Email, username);
					}
					await Clients.Client(Context.ConnectionId).SendAsync("ListCus", listcus);

					var currentUsers = await _tracker.GetOnlineUsers();
					usersOnline = await sellerDao.GetUsersOnlineAsync(username, currentUsers);
					usersOffline = await sellerDao.GetUsersOfflineAsync(username, usersOnline);

					await Clients.All.SendAsync("GetOnlineAndOfflineUsers", usersOnline, usersOffline);
				}
			}
			//var username = Context.User.FindFirstValue(ClaimTypes.Email);
			
		}
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			var emailClaim = Context.User.FindFirst(c => c.Type == ClaimTypes.Email);
			var username = emailClaim?.Value;
			var role = Context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
			if (role == "CU")
			{
				var isOffline = await _tracker.CustomerConnected(username, Context.ConnectionId);

			}
			else
			{
				var isOffline = await _tracker.UserDisconnected(username, Context.ConnectionId);
				if (isOffline)
				{
					var user = await sellerDao.GetSellersAsync(username);
					user.CountMessageNotIsRead = await messageDao.CountMessageCustomerNotIsRead(username, user.Email);
					await Clients.All.SendAsync("UserIsOffline", user);

					usersOnline.Remove(user); // Xóa người dùng khỏi danh sách usersOnline
					usersOffline.Add(user);
				}
			}
			

			
			await base.OnDisconnectedAsync(exception);
		}
	}
}
