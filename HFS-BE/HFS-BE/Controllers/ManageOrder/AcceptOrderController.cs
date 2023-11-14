using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrder;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.ManageOrder
{
    public class AcceptOrderController : BaseControllerSignalR
    {
        public AcceptOrderController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("orders/acceptOrderSeller")]
        [Authorize]
        public async Task<BaseOutputDto> AcceptOrder(OrderAcceptInputDto input)
        {
            try
            {
                var inputBL = mapper.Map<OrderAcceptInputDto, OrderProgressStatusInputDto>(input);
                inputBL.UserId = GetUserInfor().UserId;

                var cancelBL = GetBusinessLogic<AcceptOrderBusinessLogic>();
                string? customerId = "";
                var output = cancelBL.AcceptOrder(inputBL, out customerId);

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
