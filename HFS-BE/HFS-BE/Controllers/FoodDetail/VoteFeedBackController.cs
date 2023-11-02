using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.FoodDetail;
using HFS_BE.DAO.FeedBackVoteDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.FoodDetail
{
    public class VoteFeedBackController : BaseController
    {
        public VoteFeedBackController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("fooddetail/vote")]
        public BaseOutputDto Vote(VoteDaoInputDto inputDto)
        {
            try
            {
                var userInfo = this.GetUserInfor();
                if (userInfo == null || !userInfo.Role.Equals("CU"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as Customer!");
                }

                inputDto.CustomerId = userInfo.UserId;
                var busi = this.GetBusinessLogic<VoteFeedBackBusinessLogic>();
                return busi.VoteFeedBack(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
