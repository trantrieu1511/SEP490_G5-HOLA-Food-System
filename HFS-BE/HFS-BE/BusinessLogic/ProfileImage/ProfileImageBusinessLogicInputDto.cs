namespace HFS_BE.BusinessLogic.ProfileImage
{
    public class ProfileImageInputDto
    {
        public string UserId { get; set; } = string.Empty;
        public IFormFile? ImageName { get; set; }
    }
}
