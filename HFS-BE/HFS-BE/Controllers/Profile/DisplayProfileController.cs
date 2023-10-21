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
    public class DisplayProfileController : BaseController
    {
        public DisplayProfileController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("users/profile")]
        public UserProfileOutputDao Post(GetUserProfileInputDto inputDto) {
            try
            {
                var business = GetBusinessLogic<DisplayProfileBusinessLogic>();
                return business.GetProfile(inputDto);
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDao>(Constants.ResultCdFail);
            }
        }
    }
}
