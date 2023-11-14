using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Profile;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Profile
{
    public class EditProfileController : BaseController
    {
        public EditProfileController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPut("users/editprofile")]
        public BaseOutputDto Post(EditUserProfileInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<EditProfileBusinessLogic>();
                return business.EditProfile(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
