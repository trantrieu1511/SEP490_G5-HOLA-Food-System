using HFS_BE.Base;

namespace HFS_BE.DAO.CommentNewFeedDao
{
    public class CommentOutputDto
    {
        public string CustomerName { get; set; }
        public string? CommentContent { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class GetCommentByPostOutputDto : BaseOutputDto
    {
        public List<CommentOutputDto> ListComment { get; set; }
    }
}
