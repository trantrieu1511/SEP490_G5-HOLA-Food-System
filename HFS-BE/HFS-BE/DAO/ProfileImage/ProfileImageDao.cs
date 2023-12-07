using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.ProfileImage
{
    public class ProfileImageDao : BaseDao
    {
        public ProfileImageDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ProfileImageOutputDtoWrapper getUsersProfileImage(string userId)
        {
            try
            {
                ProfileImageOutputDto? profileImage = context.ProfileImages
                    .Select(pi => new ProfileImageOutputDto
                    {
                        ImageId = pi.ImageId,
                        UserId = pi.UserId,
                        Path = pi.Path,
                        IsReplaced = pi.IsReplaced
                    })
                    .SingleOrDefault(pi => pi.UserId == userId && pi.IsReplaced == false);
                var output = Output<ProfileImageOutputDtoWrapper>(Constants.ResultCdSuccess);
                output.ProfileImage = profileImage;
                return output;
            }
            catch (Exception e)
            {
                return Output<ProfileImageOutputDtoWrapper>(
                    Constants.ResultCdFail, e.Message + "\n" + e.Source
                    + "\n" + e.StackTrace + "\n" + e.InnerException);
            }
        }

        public BaseOutputDto ImportProfileImage(ProfileImageInputDto inputDto)
        {
            try
            {
                // Find user's current profile image
                var usersProfileImage = context.ProfileImages
                    .SingleOrDefault(pi => pi.UserId == inputDto.UserId
                    && pi.IsReplaced == false);

                // Add new profile image if the user have not had one yet
                if (usersProfileImage == null)
                {
                    context.ProfileImages.Add(new Models.ProfileImage
                    {
                        UserId = inputDto.UserId,
                        Path = inputDto.ImageName,
                        IsReplaced = false
                    });
                }

                // Replace old profile image with a new one
                else
                {
                    // Set isReplaced of old profile image to true
                    usersProfileImage.IsReplaced = true;
                    // Add new profile image
                    context.ProfileImages.Add(new Models.ProfileImage
                    {
                        UserId = inputDto.UserId,
                        Path = inputDto.ImageName,
                        IsReplaced = false
                    });
                }
                // Save all changes in context to db
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail,
                    e.Message + "\n" + e.Source + "\n" + e.StackTrace
                    + "\n" + e.InnerException);
            }
        }
    }
}
