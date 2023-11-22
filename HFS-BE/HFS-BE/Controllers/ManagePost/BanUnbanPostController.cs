using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePost
{
    public class BanUnbanPostController : BaseController
    {
        public BanUnbanPostController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("posts/banunban")]
        [Authorize]
        public BaseOutputDto BanOrUnbanPost(PostBanUnbanInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<BanUnbanPostBusinessLogic>();
                return business.BanUnbanPost(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

    }
}
