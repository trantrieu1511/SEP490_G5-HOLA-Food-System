using AutoMapper;
using HFS_BE.DAO.ChatMessageDao;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.DAO.SellerDao;
using HFS_BE.Hubs.Extensions;
using HFS_BE.Models;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Twilio.TwiML.Messaging;

namespace HFS_BE.Hubs
{
	public class MessageHub:Hub
	{
		IMapper _mapper;
		IHubContext<PresenceHub> _presenceHub;
		private readonly ChatMessageDao messageDao;
		private readonly SellerDao sellerDao;
		private readonly CustomerDao customerDao;
		private readonly PresenceTracker _tracker;
		public MessageHub(ChatMessageDao messageDao, SellerDao sellerDao,CustomerDao customerDao, PresenceTracker tracker, IMapper mapper, IHubContext<PresenceHub> presenceHub)
		{
			this.messageDao = messageDao;
			this.sellerDao = sellerDao;
			this.customerDao = customerDao;
			_tracker = tracker;	
			_mapper = mapper;
			_presenceHub = presenceHub;
		}

		public override async Task OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();
			var otherUser = httpContext.Request.Query["user"].ToString();
			var user2 = Context.User.GetEmail();
			var role = Context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
			if (role == "CU")
			{
				var groupName = GetGroupName(user2, otherUser);
				await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
				var messages = await messageDao.GetMessageThread(user2, otherUser);
				var group = await AddToGroup(groupName);
				await Clients.Caller.SendAsync("ReceiveMessageThread", messages);

			}
			else
			{
				var groupName = GetGroupName(user2, otherUser);
				await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
				var messages = await messageDao.GetMessageThread( otherUser, user2);
				var group = await AddToGroup(groupName);
				await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
			}
			
		}




		public override async Task OnDisconnectedAsync(Exception exception)
		{
			var group = await RemoveFromMessageGroup();
			//await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
			await base.OnDisconnectedAsync(exception);
		}
		public async Task SendMessage(CreateMessageDtoInput createMessageDto)
		{
			bool checksend = false;
			ChatMessage? chat = new ChatMessage();
			var userEmail = Context.User.GetEmail();
			var role = Context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
			
			//sendtype if =0 customer gửi seller nhận còn if =1 seller gửi customer nhận
			if (role == "CU")
			{
				var seller = await sellerDao.GetSellersAsync(createMessageDto.RecipientEmail);
				if (seller == null) throw new HubException("Not found seller");
				var customer = await customerDao.GetCustoemrAsync(userEmail);
				if (customer == null) throw new HubException("Not found customer");
				chat.SellerId = seller.SellerId;
				chat.CustomerId = customer.CustomerId;
				chat.Message = createMessageDto.Content;
				chat.SenderType = false;
				chat.SentAt = DateTime.Now;
				var groupName = GetGroupName(customer.Email, seller.Email);
				var group = await messageDao.GetMessageGroup(groupName);
				checksend=messageDao.AddMessage(chat);
				if (checksend)
				{
					var connections = await _tracker.GetConnectionsForUser(seller.Email);
					await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<ChatMessage, MessageDtoOuput>(chat));
					var listcus = await customerDao.ListCustomersendSellerbySellerAsync(seller.Email);
					await _presenceHub.Clients.All.SendAsync("ListCus", listcus);
					if (connections != null)
					{
						await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived", customer);
					}
				}
			}
			if(role == "SE")
			{
				var seller = await sellerDao.GetSellersAsync(userEmail);
				if (seller == null) throw new HubException("Not found seller");
				var customer = await customerDao.GetCustoemrAsync(createMessageDto.RecipientEmail);
				if (customer == null) throw new HubException("Not found customer");
				chat.SellerId = seller.SellerId;
				chat.CustomerId = customer.CustomerId;
				chat.Message = createMessageDto.Content;
				chat.SenderType = true;
				chat.SentAt = DateTime.Now;
				var groupName = GetGroupName(seller.Email, customer.Email);
				var group = await messageDao.GetMessageGroup(groupName);
				checksend = messageDao.AddMessage(chat);
				if (checksend)
				{
					var connections = await _tracker.GetConnectionsForUser(customer.Email);
					await Clients.Group(groupName).SendAsync("NewMessage",_mapper.Map<ChatMessage,MessageDtoOuput>(chat));
					if (connections != null)
					{
						await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived", seller);
					}
				}
			}


			
		}

		public async Task SeenMessage(string recipientEmail)
		{
			
			var userEmail = Context.User.GetEmail();
			var role = Context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
			if (role == "CU")
			{
				var messages = await messageDao.GetMessageThreadDescending(userEmail, recipientEmail);
				var groupName = GetGroupName(userEmail, recipientEmail);

				await Clients.Group(groupName).SendAsync("SeenMessageReceived", messages.FirstOrDefault().MessageId);
			}
			if (role == "SE")
			{
				var messages = await messageDao.GetMessageThreadDescending(recipientEmail, userEmail);
				var groupName = GetGroupName(recipientEmail, userEmail);

				await Clients.Group(groupName).SendAsync("SeenMessageReceived", messages.FirstOrDefault().MessageId);
			}

				
		}

		private string GetGroupName(string caller, string other)
		{
			var stringCompare = string.CompareOrdinal(caller, other) < 0;
			return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
		}


		private async Task<Group> AddToGroup(string groupName)
		{
			bool check = false;
			var group = await messageDao.GetMessageGroup(groupName);
			var connection = new Connection();
			connection.ConnectionId = Context.ConnectionId;
			var user2 = Context.User.GetEmail();
			connection.Email= user2; 
			if (group == null)
			{
				group = new Group();
				group.Name = groupName;
				connection.GroupName = group.Name;
				check =messageDao.AddGroupAndConnection(group,connection);
			}
			else
			{
				connection.GroupName = group.Name;
				check=messageDao.AddConnection(connection);
			}
		

			if (check) return group;

			throw new HubException("Failed to join group");
		}
		private async Task<Group> RemoveFromMessageGroup()
		{
			var group = await messageDao.GetGroupForConnection(Context.ConnectionId);
			var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
			bool check = messageDao.RemoveConnection(connection);

			if (check) return group;

			throw new HubException("Fail to remove from group");
		}
	}
}
