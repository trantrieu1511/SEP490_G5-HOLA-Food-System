using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrderCustomer;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageOrderCustomer
{
    public class FeedBackFoodController : BaseController
    {
        public FeedBackFoodController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("order/feedback")]
        [Authorize]
        public BaseOutputDto FeedBackFood(CreateFeedBackDaoInputDto inputDto)
        {
            try
            {
                var userInfor = this.GetUserInfor();
                if (!userInfor.Role.Equals("CU"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You are not customer!");
                }
                inputDto.CustomerId = userInfor.UserId;
                var busi = this.GetBusinessLogic<FeedBackFoodBusinessLogic>();
                return busi.FeedBackFood(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
