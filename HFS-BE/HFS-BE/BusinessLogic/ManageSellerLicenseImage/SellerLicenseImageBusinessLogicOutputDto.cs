using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.ManageSellerLicenseImage
{
    public class SellerLicenseImageOutputDto
    {
        public int ImageLicenseId { get; set; }
        public string SellerId { get; set; } = null!;
        //public string? Path { get; set; }
        public string? ImageBase64 { get; set; }
        public string? Name { get; set; }
        public string? Size { get; set; }
    }

    public class ListSellerLicenseImageOutputDto : BaseOutputDto
    {
        public List<SellerLicenseImageOutputDto> LicenseImages { get; set; }
    }
}
