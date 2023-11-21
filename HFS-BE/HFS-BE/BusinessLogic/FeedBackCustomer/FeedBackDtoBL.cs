using HFS_BE.Base;
using HFS_BE.DAO.FeedBackDao;

namespace HFS_BE.BusinessLogic.FeedBackCustomer
{
	public class FeedBackDtoBL
	{
		public int FeedbackId { get; set; }
		public int? FoodId { get; set; }
		public string? CustomerId { get; set; }
		public string? CustomerName { get; set; }
		public string? FeedbackMessage { get; set; }
		public byte? Star { get; set; }
		public DateTime? DisplayDate { get; set; }
		public int LikeCount { get; set; }
		public int DisLikeCount { get; set; }
		public bool? IsLiked { get; set; }
		public List<CustomerVoted> ListVoted { get; set; }
		public List<FeedImageOutputDto>? ImagesBase64 { get; set; } = new List<FeedImageOutputDto>();
	}
	public class FeedImageOutputDto
	{
		public int ImageId { get; set; }
		public string? ImageBase64 { get; set; }
		public string? Name { get; set; }
		public string? Size { get; set; }
	}
	public class ListFeedBackOutputDtoBS : BaseOutputDto
	{
		public List<FeedBackDtoBL> FeedBacks { get; set; }
	}
}
