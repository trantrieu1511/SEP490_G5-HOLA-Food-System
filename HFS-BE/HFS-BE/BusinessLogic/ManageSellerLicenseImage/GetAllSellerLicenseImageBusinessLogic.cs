using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.DAO.SellerLicenseImageDao;
using HFS_BE.Models;
using HFS_BE.Utils.IOFile;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageSellerLicenseImage
{
    public class GetAllSellerLicenseImageBusinessLogic : BaseBusinessLogic
    {
        public GetAllSellerLicenseImageBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListSellerLicenseImageOutputDto GetAllSellerLicenseImages(string email, string sellerId)
        {
            try
            {
                var dao = CreateDao<SellerLicenseImageDao>();
                var outputDao = dao.GetAllSellerLicenseImages(sellerId);
                var outputBL = mapper.Map<DAO.SellerLicenseImageDao.ListSellerLicenseImageOutputDto, ListSellerLicenseImageOutputDto>(outputDao);

                if (outputDao.LicenseImages == null || outputDao.LicenseImages.Count < 1)
                {
                    return Output<ListSellerLicenseImageOutputDto>(Constants.ResultCdSuccess);
                }

                foreach (var image in outputDao.LicenseImages)
                {
                    int index = outputDao.LicenseImages.IndexOf(image);
                    ImageFileConvert.ImageOutputDto? imageInfor = null;

                    imageInfor = ImageFileConvert.ConvertFileToBase64(email, image.Path, 6);

                    if (imageInfor == null)
                        continue;

                    outputBL.LicenseImages[index] = mapper.Map<ImageFileConvert.ImageOutputDto, SellerLicenseImageOutputDto>(imageInfor);
                    outputBL.LicenseImages[index].ImageLicenseId = image.ImageLicenseId;
                    outputBL.LicenseImages[index].SellerId = image.SellerId;
                    //outputBL.LicenseImages[index].ImageBase64 = imageInfor.ImageBase64;
                    //outputBL.LicenseImages[index].ImageBase64 = imageInfor.ImageBase64;
                    //outputBL.LicenseImages[index].ImageBase64 = imageInfor.ImageBase64;
                }

                return outputBL;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
