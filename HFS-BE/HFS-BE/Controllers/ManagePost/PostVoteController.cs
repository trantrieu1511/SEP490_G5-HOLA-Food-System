using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.FoodDetail;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.DAO.PostVoteDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePost
{
    public class PostVoteController : BaseController
    {
        public PostVoteController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("posts/vote")]
        [Authorize]
        public BaseOutputDto Vote(PostVoteDaoInputDto inputDto)
        {
            try
            {
                var userInfo = this.GetUserInfor();
                if (!userInfo.Role.Equals("CU"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as Customer!");
                }

                inputDto.CustomerId = userInfo.UserId;
                var busi = this.GetBusinessLogic<VotePostBusinessLogic>();
                return busi.Vote(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
