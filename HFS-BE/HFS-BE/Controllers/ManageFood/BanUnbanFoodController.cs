using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.ManageFood
{
    public class BanUnbanFoodController : BaseControllerSignalR
    {
        public BanUnbanFoodController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("foods/banunban")]
        [Authorize]
        public async Task<BaseOutputDto> BanUnbanFood(FoodBanUnbanInputDto inputDto) {
            try
            {
                var business = GetBusinessLogic<BanUnbanFoodBusinessLogic>();
                string? sellerId;
                var output = business.BanUnbanFood(inputDto, GetUserInfor().UserId, out sellerId);

                if (output.Success)
                {
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    await notifyHub.Clients.Group(sellerId).SendAsync("notification");
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
