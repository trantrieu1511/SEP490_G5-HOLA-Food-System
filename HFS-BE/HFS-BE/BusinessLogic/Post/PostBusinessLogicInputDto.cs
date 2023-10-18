using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Post
{
    public class PostCreateInputDto
    {
        public string? PostContent { get; set; }
        public IReadOnlyList<IFormFile> Images { get; set; }

        public UserDto UserDto { get; set; }
    }
}
