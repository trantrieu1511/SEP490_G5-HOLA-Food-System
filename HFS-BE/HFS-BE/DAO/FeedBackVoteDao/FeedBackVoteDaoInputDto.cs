namespace HFS_BE.DAO.FeedBackVoteDao
{
    public class VoteDaoInputDto
    {
        public int FeedBackId { get; set; }
        public string? CustomerId { get; set; }
        public bool? IsLike { get; set; }
    }
}
