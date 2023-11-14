namespace HFS_BE.DAO.CommentNewFeedDao
{
    public class CreateCommentInputDto
    {
        public int PostId { get; set; }
        public string CustomerId { get; set; } = null!;
        public string? CommentContent { get; set; }
    }

    public class GetCommentInputDto
    {
        public int PostId { get; set; }
        
    }
}
