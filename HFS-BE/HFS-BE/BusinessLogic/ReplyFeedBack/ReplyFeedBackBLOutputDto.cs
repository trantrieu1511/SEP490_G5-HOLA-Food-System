using HFS_BE.Base;
using HFS_BE.BusinessLogic.FeedBackCustomer;

namespace HFS_BE.BusinessLogic.ReplyFeedBack
{
	public class ReplyFeedBackBLOutputDto
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

		public List<FeedSellerImageOutputDto>? ImagesBase64 { get; set; } = new List<FeedSellerImageOutputDto>();
	}

	public class FeedSellerImageOutputDto
	{
		public int ImageId { get; set; }
		public string? ImageBase64 { get; set; }
		public string? Name { get; set; }
		public string? Size { get; set; }
	}
	public class ListFeedBackbySellerOutputDtoBL : BaseOutputDto
	{
		public List<ReplyFeedBackBLOutputDto> FeedBacks { get; set; }
	}
}
