using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageMenuReport;
using HFS_BE.DAO.MenuReportDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.ManageMenuReport
{
    public class ApproveNotApproveMenuReportController : BaseControllerSignalR
    {
        public ApproveNotApproveMenuReportController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("menureports/approvenotapprovefoodreport")]
        [Authorize]
        public async Task<BaseOutputDto> ApproveNotApproveFoodReport(ApproveNotApproveFoodReportInputDto inputDto)
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("MM"))
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a menu moderator before executing this API.");
                }
                var business = GetBusinessLogic<ApproveNotApproveFoodReportBusinessLogic>();
                string? sellerId;
                var output = business.ApproveNotApproveFoodReport(inputDto, GetUserInfor().UserId, out sellerId);

                if (output.Success)
                {
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    await notifyHub.Clients.Group(inputDto.ReportBy).SendAsync("notification");
                    
                    /*//approved -> send seller || not approved-> no
                    if(inputDto.IsApproved)
                        await notifyHub.Clients.Group(sellerId).SendAsync("notification");*/
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
