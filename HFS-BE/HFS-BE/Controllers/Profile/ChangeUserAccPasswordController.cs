using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Profile;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Profile
{
    public class ChangeUserAccPasswordController : BaseController
    {
        public ChangeUserAccPasswordController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("users/changeaccountpassword")]
        [Authorize]
        public BaseOutputDto ChangeUserAccountPassword(ChangeUserAccountPasswordInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<ChangeUserAccPasswordBusinessLogic>();
                return business.ChangeUserAccountPassword(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
