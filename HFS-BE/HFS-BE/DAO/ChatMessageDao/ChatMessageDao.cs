using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using Mailjet.Client.Resources;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.ChatMessageDao
{
	public class ChatMessageDao : BaseDao
	{
		public ChatMessageDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public bool AddMessage(ChatMessage message)
		{
			try
			{
				context.ChatMessages.Add(message);
				context.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		
		}

		public bool AddGroup(Group group)
		{
			try {
				context.Groups.Add(group);
				context.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}
		public bool AddGroupAndConnection(Group group,Connection connection)
		{
			try
			{
				context.Groups.Add(group);
				context.Connections.Add(connection);
				context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public bool AddConnection(Connection connection)
		{
			try
			{
			
				context.Connections.Add(connection);
				context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public async Task<Connection> GetConnection(string connectionId)
		{
			return await context.Connections.FindAsync(connectionId);
		}

		public async Task<Group> GetGroupForConnection(string connectionId)
		{
			return await context.Groups.Include(x => x.Connections)
				.Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
				.FirstOrDefaultAsync();
		}
		public async Task<Group> GetMessageGroup(string groupName)
		{
			return await context.Groups.Include(x => x.Connections).FirstOrDefaultAsync(x => x.Name == groupName);
		}

		public async Task<IEnumerable<MessageDtoOuput>> GetMessageThread(string EmailCustomer, string EmailSeller)//chưa mapper
		{
			var messages = await context.ChatMessages.Include(s=>s.Seller).Include(s=>s.Customer)
				.Where(m => m.Customer.Email == EmailCustomer && m.Seller.Email == EmailSeller)
				.OrderBy(m => m.SentAt)
				.ToListAsync();
			var output = mapper.Map<List<ChatMessage>, List<MessageDtoOuput>>(messages);
			

			return output;
		}

		public async Task<IEnumerable<MessageDtoOuput>> GetMessageThreadDescending(string EmailCustomer, string EmailSeller)//chưa mapper
		{
			var messages = await context.ChatMessages.Include(s => s.Seller).Include(s => s.Customer)
				.Where(m => m.Customer.Email == EmailCustomer && m.Seller.Email == EmailSeller)
				.OrderByDescending(m => m.SentAt)
				.ToListAsync();
			var output = mapper.Map<List<ChatMessage>, List<MessageDtoOuput>>(messages);


			return output;
		}

		public bool RemoveConnection(Connection connection)
		{
			try
			{

				context.Connections.Remove(connection);
				context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
