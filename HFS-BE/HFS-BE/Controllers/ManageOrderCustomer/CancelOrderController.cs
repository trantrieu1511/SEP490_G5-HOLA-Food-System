using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrderCustomer;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.ManageOrderCustomer
{
    public class CancelOrderController : BaseControllerSignalR
    {
        public CancelOrderController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("order/cancel")]
        [Authorize]
        public async Task<BaseOutputDto> CancelOrder(OrderCustomerDaoInputDto inputDto)
        {
            try
            {
                var userInfo = this.GetUserInfor();
                if (!userInfo.Role.Equals("CU"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You are not customer!");
                }
                //inputDto.CustomerId = userInfo.UserId;
                inputDto.CustomerId = userInfo.UserId;
                var busi = this.GetBusinessLogic<CancelOrderBusinessLogic>();

                string? sellerId;
                var output = busi.CancelOrder(inputDto, out sellerId);

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
