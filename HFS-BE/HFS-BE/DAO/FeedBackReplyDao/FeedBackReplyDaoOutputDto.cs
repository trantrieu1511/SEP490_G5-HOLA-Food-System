using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.DAO.FeedBackReplyDao
{
    public class GetReplyByFeedBackIdDaoOutputDto : BaseOutputDto
    {
        public List<FeedBackReplyDaoOutputDto> ReplyList { get; set; }
    }
	public class ListFeedBackbySellerDaoOutputDto : BaseOutputDto
	{
		public List<FeedBackBySellerDaoOutputDto> FeedBacks { get; set; }
	}
	public class FeedBackReplyDaoOutputDto
    {
        public int ReplyId { get; set; }
        public string UserId { get; set; } = null!;
        public string UserName { get; set; }
        public int FeedbackId { get; set; }
        public string? ReplyMessage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
	public class FeedBackBySellerDaoOutputDto
	{
		public int FeedbackId { get; set; }
		public int? FoodId { get; set; }
		public string? FoodName { get; set; }
		public string? CustomerId { get; set; }
		public string? CustomerName { get; set; }
		public string? FeedbackMessage { get; set; }
		public byte? Star { get; set; }
		public DateTime? DisplayDate { get; set; }
		public int LikeCount { get; set; }
		public int DisLikeCount { get; set; }
		public bool CheckReply { get; set; }
		public List<FeedBackImage> Images { get; set; }
	}
	
}
