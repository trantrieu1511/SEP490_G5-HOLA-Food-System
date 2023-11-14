using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.Cart
{
    public class CheckOutCartController : BaseControllerSignalR
    {
        public CheckOutCartController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("checkout/createorder")]
        [Authorize(Roles = "CU")]
        public async Task<BaseOutputDto> CheckOutCart(CheckOutCartInputDto inputDto)
        {
            try
            {
                inputDto.CustomerId = this.GetUserInfor().UserId;
                var busi = this.GetBusinessLogic<CheckOutBusinessLogic>();
                var result = busi.CheckOutCart(inputDto);

                if (!result.Success)
                {
                    var output = Output<BaseOutputDto>(Constants.ResultCdFail);
                    output.Errors = result.Errors;
                    output.Message = result.Message;
                    return output;
                }

                //notify for seller
                var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                // refresh data of seller
                var dataRealTimeHub = _hubContextFactory.CreateHub<DataRealTimeHub>();

                foreach (var shop in inputDto.ListShop)
                {
                    await notifyHub.Clients.Group(shop.ShopId).SendAsync("notification");
                    await dataRealTimeHub.Clients.Group(shop.ShopId).SendAsync("orderSellerRealTime");
                }

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
