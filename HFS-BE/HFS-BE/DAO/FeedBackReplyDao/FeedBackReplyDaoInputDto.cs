namespace HFS_BE.DAO.FeedBackReplyDao
{
    public class GetReplyByFeedBackIdDaoInputDto
    {
        public int FeedBackId { get; set; }
    }
	public class FeedBackbySellerDaoInputDto
	{
		public string? SellerId { get; set; } 
	}
	public class CreateFeedBackbySellerDaoInputDto
	{
		public string? CustomerId { get; set; }
		public string? SellerId { get; set; }
		public int FeedbackId { get; set; }
		public string? ReplyMessage { get; set; }
	}
}
