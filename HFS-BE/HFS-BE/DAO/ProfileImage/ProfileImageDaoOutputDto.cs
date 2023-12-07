using HFS_BE.Base;

namespace HFS_BE.DAO.ProfileImage
{
    public class ProfileImageOutputDto
    {
        public int ImageId { get; set; }
        public string UserId { get; set; } = null!;
        public string Path { get; set; } = null!;
        public bool IsReplaced { get; set; }
    }

    public class ProfileImageOutputDtoWrapper : BaseOutputDto
    {
        public ProfileImageOutputDto ProfileImage { get; set; }
    }
}
