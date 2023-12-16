namespace HFS_BE.DAO.PostVoteDao
{
    public class PostVoteDaoInputDto
    {
        public int PostId { get; set; }
        public string? CustomerId { get; set; }
        public bool? IsLike { get; set; }
    }
}
