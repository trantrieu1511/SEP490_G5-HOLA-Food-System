using HFS_BE.Base;

namespace HFS_BE.DAO.FeedBackVoteDao
{
    public class GetVoteDaoOutputDto : BaseOutputDto
    {
        public int? FeedBackId { get; set; }
        public string? CustomerId { get; set; }
        public bool? IsLike { get; set; }
    }
}
