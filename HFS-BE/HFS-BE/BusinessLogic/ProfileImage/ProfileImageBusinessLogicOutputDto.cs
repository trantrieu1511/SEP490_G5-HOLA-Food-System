using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.ProfileImage
{
    public class ProfileImageOutputDto
    {
        public int ImageId { get; set; }
        public string UserId { get; set; } = null!;
        public string Path { get; set; } = null!;
        public bool IsReplaced { get; set; }
        public string? ImageBase64 { get; set; } // Base64 of the image name (with its path)
        //public string? Name { get; set; }
        public string? Size { get; set; }
    }

    public class ProfileImageOutputDtoWrapper : BaseOutputDto
    {
        public ProfileImageOutputDto ProfileImage { get; set; }
    }
}
