using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Dao.PostDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.ManagePost
{
    public class BanUnbanPostController : BaseControllerSignalR
    {
        public BanUnbanPostController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("posts/banunban")]
        [Authorize]
        public async Task<BaseOutputDto> BanOrUnbanPost(PostBanUnbanInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<BanUnbanPostBusinessLogic>();
                string? sellerId;
                var output = business.BanUnbanPost(inputDto, GetUserInfor().UserId, out sellerId);

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
