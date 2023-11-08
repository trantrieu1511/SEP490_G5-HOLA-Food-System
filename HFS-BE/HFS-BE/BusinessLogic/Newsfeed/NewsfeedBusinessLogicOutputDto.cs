using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Newsfeed
{
    public class NewsfeedOutputDto
    {
        public int PostId { get; set; }
        public string SellerFullName { get; set; } = string.Empty;
        public string? PostContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<PostImageOutputDto> ImagesBase64 { get; set; } = new List<PostImageOutputDto>();
        //public List<ProfileImage> Avatars { get; set; } = new List<ProfileImage>();
    }

    public class PostImageOutputDto
    {
        public int ImageId { get; set; }
        public string ImageBase64 { get; set; } = string.Empty;
        //public string? Name { get; set; }
        //public string? Size { get; set; }
    }

    public class ListNewsfeedOutputDto : BaseOutputDto
    {
        public List<NewsfeedOutputDto> News { get; set; } = new List<NewsfeedOutputDto>();
    }
}
