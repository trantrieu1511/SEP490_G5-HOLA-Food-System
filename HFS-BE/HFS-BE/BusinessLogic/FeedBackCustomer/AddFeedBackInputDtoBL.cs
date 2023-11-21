using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.FeedBackCustomer
{
	public class AddFeedBackInputDtoBL
	{
		public int? FoodId { get; set; }
		public string? FeedbackMessage { get; set; }
		public byte? Star { get; set; }
		public IReadOnlyList<IFormFile>? Images { get; set; }

		public UserDto UserDto { get; set; }
	}
}
