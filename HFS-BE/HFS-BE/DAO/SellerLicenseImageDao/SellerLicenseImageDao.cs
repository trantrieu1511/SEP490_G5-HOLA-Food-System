using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.SellerLicenseImageDao
{
    public class SellerLicenseImageDao : BaseDao
    {
        public SellerLicenseImageDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListSellerLicenseImageOutputDto GetAllSellerLicenseImages(string sellerId)
        {
            try
            {
                var licenseImages = context.SellerLicenseImages.
                    Where(sli => sli.SellerId.Equals(sellerId) && sli.IsReplaced == false)
                    .Select(sli => new SellerLicenseImageOutputDto
                    {
                        ImageLicenseId = sli.ImageLicenseId,
                        SellerId = sli.SellerId,
                        Path = sli.Path
                    })
                    .ToList();
                var output = Output<ListSellerLicenseImageOutputDto>(Constants.ResultCdSuccess);
                output.LicenseImages = licenseImages;
                return output;
            }
            catch (Exception e)
            {
                return Output<ListSellerLicenseImageOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }

        public List<SellerLicenseImage>? GetAllLicenseImageBySellerId(string sellerId)
        {
            var output = context.SellerLicenseImages.Where(
                    sli => sli.SellerId == sellerId
                ).ToList();
            if (output.Count > 0)
                return output;
            return null;
        }

        public BaseOutputDto UpdateImageInfor(SellerLicenseImage image)
        {
            try
            {
                var imageModel = context.SellerLicenseImages.FirstOrDefault(
                    img => img.ImageLicenseId == image.ImageLicenseId
                );

                imageModel.Path = image.Path;
                imageModel.IsReplaced = image.IsReplaced;

                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto AddNewImage(SellerLicenseImage image)
        {
            try
            {
                context.SellerLicenseImages.Add(image);
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
