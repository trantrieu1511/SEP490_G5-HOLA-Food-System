using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Profile;
using HFS_BE.DAO.UserDao;
using HFS_BE.DAO.UserDao.CustomerDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Profile
{
    public class DisplayProfileController : BaseController
    {
        public DisplayProfileController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("users/profile")]
        public UserProfileOutputDto Post() {
            try
            {
                var business = GetBusinessLogic<DisplayProfileBusinessLogic>();
                int userId = GetUserInfor().UserId;

                return business.GetProfile(userId);
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
