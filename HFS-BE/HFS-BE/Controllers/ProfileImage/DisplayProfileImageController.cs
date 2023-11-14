using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ProfileImage;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ProfileImage
{
    public class DisplayProfileImageController : BaseController
    {
        public DisplayProfileImageController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("profileImage/getProfileImage")]
        public ProfileImageOutputDtoWrapper GetProfileImage()
        {
            try
            {
                if (GetUserInfor().UserId == null)
                {
                    return Output<ProfileImageOutputDtoWrapper>(Constants.ResultCdFail, "Please login first before using this API.");
                }
                var business = GetBusinessLogic<GetProfileImageBusinessLogic>();
                return business.GetProfileImage(GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<ProfileImageOutputDtoWrapper>(Constants.ResultCdFail);
            }
        }
    }
}
