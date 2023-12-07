using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.DAO.ProfileImage;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;
using Microsoft.Extensions.Hosting;

namespace HFS_BE.BusinessLogic.ProfileImage
{
    public class GetProfileImageBusinessLogic : BaseBusinessLogic
    {
        public GetProfileImageBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ProfileImageOutputDtoWrapper GetProfileImage(string userId)
        {
            try
            {
                //if (userId == null)
                //{
                //    return Output<ProfileImageOutputDtoWrapper>(Constants.ResultCdFail, "Please login before using this API.");
                //}
                var dao = CreateDao<ProfileImageDao>();
                var daoOutput = dao.getUsersProfileImage(userId);
                //if (!daoOutput.Success)
                //{
                //    return Output<ProfileImageOutputDtoWrapper>(Constants.ResultCdFail);
                //}
                if (daoOutput.ProfileImage == null)
                {
                    return Output<ProfileImageOutputDtoWrapper>(Constants.ResultCdFail, "User have not imported any profile images yet.");
                }
                var output = mapper.Map<DAO.ProfileImage.ProfileImageOutputDtoWrapper, ProfileImageOutputDtoWrapper>(daoOutput);

                ImageFileConvert.ImageOutputDto? convertedImageInfo = null;
                convertedImageInfo = ImageFileConvert.ConvertFileToBase64(daoOutput.ProfileImage.UserId, daoOutput.ProfileImage.Path, 3);

                output.ProfileImage.ImageBase64 = convertedImageInfo.ImageBase64;
                //output.ProfileImage.Name = convertedImageInfo.Name;
                output.ProfileImage.Size = convertedImageInfo.Size;

                return output;
            }
            catch (Exception e)
            {
                return Output<ProfileImageOutputDtoWrapper>(Constants.ResultCdFail, e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\n" + e.InnerException);
            }
        }
    }
}
