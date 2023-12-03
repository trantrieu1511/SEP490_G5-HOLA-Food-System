using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageSellerLicenseImage
{
    public class EditLicenseImageInputDto
    {
        //public int ImageLicenseId { get; set; }
        public IReadOnlyList<IFormFile>? Images { get; set; }

        public UserDto? UserDto { get; set; }
    }
}
