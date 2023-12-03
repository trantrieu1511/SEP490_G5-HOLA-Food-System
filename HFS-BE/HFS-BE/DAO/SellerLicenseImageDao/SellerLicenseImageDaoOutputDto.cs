using HFS_BE.Base;

namespace HFS_BE.DAO.SellerLicenseImageDao
{
    public class SellerLicenseImageOutputDto
    {
        public int ImageLicenseId { get; set; }
        public string SellerId { get; set; } = null!;
        public string? Path { get; set; }
        public bool IsReplaced { get; set; }
    }

    public class ListSellerLicenseImageOutputDto : BaseOutputDto
    {
        public List<SellerLicenseImageOutputDto> LicenseImages { get; set; } = new List<SellerLicenseImageOutputDto>();
    }
}
