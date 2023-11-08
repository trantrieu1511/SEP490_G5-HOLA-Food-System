using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.DAO.NewsfeedDao
{
    public class NewsfeedOutputDto
    {
        public int PostId { get; set; }
        public string SellerId { get; set; } = string.Empty;
        public string SellerFullName { get; set; } = string.Empty;
        public string? PostContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<PostImage> Images { get; set; } = new List<PostImage>();
        //public List<ProfileImage> Avatars { get; set; } = new List<ProfileImage>();
    }

    public class ListNewsfeedOutputDto : BaseOutputDto
    {
        public List<NewsfeedOutputDto> News { get; set; } = new List<NewsfeedOutputDto>();
    }
}
