namespace HFS_BE.DAO.FeedBackDao
{
    public class GetFeedBackByFoodIdDaoInputDto
    {
        public int? FoodId { get; set; }
    }

    public class CreateFeedBackDaoInputDto
    {
        public int? FoodId { get; set; }
        public string? CustomerId { get; set; }
        public string? FeedbackMessage { get; set; }
        public byte? Star { get; set; }

		public List<string>? Images { get; set; }
	}
}
