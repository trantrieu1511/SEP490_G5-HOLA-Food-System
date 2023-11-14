using HFS_BE.Base;

namespace HFS_BE.DAO.ChatMessageDao
{
	public class MessageDtoOuput:BaseOutputDto
	{
		public int MessageId { get; set; }
		public string CustomerId { get; set; } = null!;
		public string EmailCustomer { get; set; }
		public string SellerId { get; set; } = null!;
		public string EmailSeller{ get; set; }
		public bool SenderType { get; set; }
		public string Message { get; set; } = null!;
		public DateTime SentAt { get; set; }
	}
}
