using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.FoodImageDao;
using HFS_BE.DAO.SellerLicenseImageDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.ManageSellerLicenseImage
{
    public class EditSellerLicenseImageBusinessLogic : BaseBusinessLogic
    {
        public EditSellerLicenseImageBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto EditLicenseImage(EditLicenseImageInputDto inputDto)
        {
            try
            {
                // ** check file list iamge
                // * get list image by foodId
                var licenseImageDao = CreateDao<SellerLicenseImageDao>();
                var listImagesModel = licenseImageDao.GetAllLicenseImageBySellerId(inputDto.UserDto.UserId);

                // if img FE request && image in DB =  empty -> no change -> finish
                if ((inputDto.Images == null || inputDto.Images.Count <= 0) && listImagesModel == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                // if img FE request = empty && image in DB > 0   -> Remove -> finish
                if ((inputDto.Images == null || inputDto.Images.Count <= 0) && listImagesModel != null)
                {
                    foreach (var img in listImagesModel)
                    {
                        img.IsReplaced = true;
                        licenseImageDao.UpdateImageInfor(img);
                    }
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                // if img FE request > 0 && image in DB < 0   -> Add new -> finish
                if ((inputDto.Images != null && inputDto.Images.Count > 0) && listImagesModel == null)
                {
                    SaveAndAddImage(inputDto.Images, inputDto.UserDto);
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }

                if (inputDto.Images != null && listImagesModel != null)
                {
                    // * check what image
                    // if: new (Add new)
                    // else if: remove (update isReplaced -> true)
                    // else: old(don't care)

                    // -- remove (update isReplaced -> true)
                    var removeImages = listImagesModel.Where(img =>
                        // check image in DB (1) vs img FE request (2)
                        // if (1) not in (2) => true
                        inputDto.Images.FirstOrDefault(i => i.FileName.Equals(img.Path)) == null ? true : false
                    ).ToList();
                    // - update IsReplaced -> true
                    foreach (var img in removeImages)
                    {
                        img.IsReplaced = true;
                        licenseImageDao.UpdateImageInfor(img);
                    }

                    // -- new (Add new)
                    var newImages = inputDto.Images.Where(img =>
                       // check  img FE request (1) vs image in DB (2)
                       // if (1) not in (2) => true
                       listImagesModel.FirstOrDefault(i => i.Path.Equals(img.FileName)) == null ? true : false
                    ).ToList();

                    // - Save image to db and Add new license img of user to Resource path
                    SaveAndAddImage(newImages, inputDto.UserDto);
                }
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Add the images to the server's local path (Resource/...) and save it to db
        /// </summary>
        /// <param name="newImages"></param>
        /// <param name="user"></param>
        private void SaveAndAddImage(IReadOnlyList<IFormFile> newImages, UserDto user)
        {
            var licenseImageDao = CreateDao<SellerLicenseImageDao>();
            // - Save to resource path
            List<string> fileNames = ReadSaveImage.SaveLicenseImages(newImages, user, 6);
            // - Add new license image to db
            foreach (var img in fileNames)
            {
                var licenseImgModel = new SellerLicenseImage
                {
                    SellerId = user.UserId,
                    Path = img,
                    IsReplaced = false
                };

                licenseImageDao.AddNewImage(licenseImgModel);
            }
        }
    }
}
