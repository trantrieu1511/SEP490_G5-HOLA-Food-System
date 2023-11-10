using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrder;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace HFS_BE.Controllers.ManageOrder
{
    public class CommissionInternalShipperController : BaseControllerSignalR
    {
        public CommissionInternalShipperController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("orders/internalShipperOrderSeller")]
        [Authorize]
        public async Task<BaseOutputDto> CommissionInternal(OrderInternalInputDto input)
        {
            try
            {
                var inputBL = mapper.Map<OrderInternalInputDto, OrderInternalShipInputDto>(input);
                inputBL.User = GetUserInfor();

                var internalBL = GetBusinessLogic<CommissionInternalShipperBL>();
                var result = internalBL.CommissionInternal(inputBL);
                if (result.Success)
                {
                    //notify for shipper
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();

                    await notifyHub.Clients.Group(input.ShipperId).SendAsync("notification");
                    

                    // refresh data of shipperId
                    var dataRealTimeHub = _hubContextFactory.CreateHub<DataRealTimeHub>();
                    await dataRealTimeHub.Clients.Group(input.ShipperId).SendAsync("shipperRealTime");
                    //await dataRealTimeHub.Clients.Group("SE00000001").SendAsync("dataRealTime");

                }
                return result;
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

    }
}
