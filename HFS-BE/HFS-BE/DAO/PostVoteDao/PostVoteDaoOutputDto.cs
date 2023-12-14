using HFS_BE.Base;

namespace HFS_BE.DAO.PostVoteDao
{
    public class GetPostVoteDaoOutputDto : BaseOutputDto
    {
        public int? PostId { get; set; }
        public string? CustomerId { get; set; }
        public bool? IsLike { get; set; }
    }
}
