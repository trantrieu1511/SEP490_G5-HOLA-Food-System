using HFS_BE.Base;

namespace HFS_BE.DAO.FeedBackDao
{
    public class FeedBackDaoOutputDto
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
    }

    public class CustomerVoted
    {
        public string VoteBy { get; set; } = null!;
    }

    public class GetFeedBackByFoodIdDaoOutputDto : BaseOutputDto
    {
        public List<FeedBackDaoOutputDto> FeedBacks { get; set; }
    }
}
