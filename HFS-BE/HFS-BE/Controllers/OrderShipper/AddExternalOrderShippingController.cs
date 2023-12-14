using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.OrderShipper;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.OrderShipper
{
    public class AddExternalOrderShippingController : BaseControllerSignalR
    {
        public AddExternalOrderShippingController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }
        [HttpPost("shipper/addExternalShipping")]
        public async Task<BaseOutputDto> AddExternalShipping(ExternalShippingOrder inputDto)
        {
            try
            {
                var busi = GetBusinessLogic<AddExternalOrderShippingBL>();

                string? sellerId = "";
                string? customerId = "";

                var output = busi.AddExternalShipping(new ExternalShippingOrderBL
                {
                    OrderId = inputDto.OrderId,
                    UserDto = GetUserInfor()
                }, out sellerId, out customerId);

                if (output.Success)
                {
                    //notify for customer
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    await notifyHub.Clients.Group(customerId).SendAsync("notification");
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
