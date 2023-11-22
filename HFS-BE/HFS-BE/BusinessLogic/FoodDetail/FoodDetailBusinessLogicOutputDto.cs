using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.FoodDetail
{
    public class FeedBackOutputDto
    {
        public int FeedbackId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? FeedbackMessage { get; set; }
        public byte? Star { get; set; }
        public string? DisplayDate { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public bool? CanReply { get; set; }
        public bool? IsLiked { get; set; }
        public List<FeedBackReplyOutputDto> ListReply { get; set; }
    }

    public class FeedBackReplyOutputDto
    {
        public int ReplyId { get; set; }
        public string UserId { get; set; } = null!;
        public string UserName { get; set; }
        public string? ReplyMessage { get; set; }
        public string? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class GetFeedBackOutputDto : BaseOutputDto
    {
        public List<FeedBackOutputDto> FeedBacks { get; set; }
    }
}
