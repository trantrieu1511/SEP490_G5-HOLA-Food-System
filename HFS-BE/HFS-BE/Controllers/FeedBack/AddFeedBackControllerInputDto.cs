namespace HFS_BE.Controllers.FeedBack
{
	public class AddFeedBackControllerInputDto
	{
		public int? FoodId { get; set; }
		public string? FeedbackMessage { get; set; }
		public byte? Star { get; set; }
		public IReadOnlyList<IFormFile>? Images { get; set; }
	}
}
