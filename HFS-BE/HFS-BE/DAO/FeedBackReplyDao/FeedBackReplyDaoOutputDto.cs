using HFS_BE.Base;

namespace HFS_BE.DAO.FeedBackReplyDao
{
    public class GetReplyByFeedBackIdDaoOutputDto : BaseOutputDto
    {
        public List<FeedBackReplyDaoOutputDto> ReplyList { get; set; }
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
}
