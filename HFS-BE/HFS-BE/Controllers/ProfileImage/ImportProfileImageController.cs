using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ProfileImage;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ProfileImage
{
    public class ImportProfileImageController : BaseController
    {
        public ImportProfileImageController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("profileImage/importProfileImage")]
        public BaseOutputDto ImportProfileImage(IFormFile profileImage)
        {
            try
            {
                var business = GetBusinessLogic<ImportProfileImageBusinessLogic>();
                var profileImageInputDto = new ProfileImageInputDto
                {
                    UserId = GetUserInfor().UserId,
                    ImageName = profileImage
                };
                return business.ImportProfileImage(profileImageInputDto);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
