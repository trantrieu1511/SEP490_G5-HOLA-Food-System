using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePost
{
    public class PostCreateInputDto
    {
        public string? PostContent { get; set; }
        public IReadOnlyList<IFormFile> Images { get; set; }

        public UserDto UserDto { get; set; }
    }

    public class PostUpdateInputDto
    {
        public int PostId { get; set; }
        public string? PostContent { get; set; }
        public IReadOnlyList<IFormFile> Images { get; set; }

        public UserDto UserDto { get; set; }
    }
}
