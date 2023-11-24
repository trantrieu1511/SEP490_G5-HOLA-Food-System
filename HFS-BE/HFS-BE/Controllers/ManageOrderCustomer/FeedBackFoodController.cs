using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrderCustomer;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.ManageOrderCustomer
{
    public class FeedBackFoodController : BaseControllerSignalR
    {
        public FeedBackFoodController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("order/feedback")]
        //[Authorize]
        public async Task<BaseOutputDto> FeedBackFood(CreateFeedBackDaoInputDto inputDto)
        {
            try
            {
                //var userInfor = this.GetUserInfor();
                //if (!userInfor.Role.Equals("CU"))
                //{
                //    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You are not customer!");
                //}
                //inputDto.CustomerId = userInfor.UserId;
                inputDto.CustomerId = "CU00000001";
                var busi = this.GetBusinessLogic<FeedBackFoodBusinessLogic>();
                string? sellerId;
                var output = busi.FeedBackFood(inputDto, out sellerId);
                if (output.Success)
                {
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    await notifyHub.Clients.Group(sellerId).SendAsync("notification");
                }

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
