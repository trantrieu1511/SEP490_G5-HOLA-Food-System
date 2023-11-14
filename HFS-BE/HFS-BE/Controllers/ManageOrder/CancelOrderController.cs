using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrder;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.ManageOrder
{
    public class CancelOrderController : BaseControllerSignalR
    {
        public CancelOrderController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("orders/cancelOrderSeller")]
        [Authorize]
        public async Task<BaseOutputDto> CancelOrder(OrderCancelInputDto input)
        {
            try
            {
                var inputBL = mapper.Map<OrderCancelInputDto, OrderProgressCancelInputDto>(input);
                inputBL.UserId = GetUserInfor().UserId;

                var cancelBL = GetBusinessLogic<CancelOrderBusinessLogic>();
                string? customerId = "";
                var output = cancelBL.CancelOrder(inputBL, out customerId);

                if (output.Success)
                {
                    //notify for customer
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    await notifyHub.Clients.Group(customerId).SendAsync("notification");
                }

                return output;
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
