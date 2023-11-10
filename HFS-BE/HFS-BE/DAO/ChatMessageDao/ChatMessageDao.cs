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
		public void AddMessage(ChatMessage message)
		{
			context.ChatMessages.Add(message);
		}
		public void AddGroup(Group group)
		{
			context.Groups.Add(group);
		}


	}
}
