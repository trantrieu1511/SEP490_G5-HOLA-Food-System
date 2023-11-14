using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrder;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.ManageOrder
{
    public class CommissionExternalShipperController : BaseControllerSignalR
    {
        public CommissionExternalShipperController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("orders/externalShipperOrderSeller")]
        [Authorize]
        public BaseOutputDto CommissionExternal(OrderExternalInputDto input)
        {
            try
            {
                var inputBL = mapper.Map<OrderExternalInputDto, OrderExternalShipInputDto>(input);
                inputBL.UserId = GetUserInfor().UserId;

                var internalBL = GetBusinessLogic<CommissionExternalShipperBL>();
                return internalBL.CommissionExternal(inputBL);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
