﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePostReport;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.ManagePostReport
{
    public class ApproveNotApprovePostReportController : BaseControllerSignalR
    {
        public ApproveNotApprovePostReportController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("postreports/approvenotapprovepostreport")]
        [Authorize]
        public async Task<BaseOutputDto> ApproveNotApprovePostReport(ApproveNotApprovePostReportInputDto inputDto)
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("PM"))
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a post moderator before executing this API.");
                }
                var business = GetBusinessLogic<ApproveNotApprovePostReportBusinessLogic>();

                string? sellerId;
                var output = business.ApproveNotApprovePostReport(inputDto, GetUserInfor().UserId,out sellerId);
                if (output.Success)
                {
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    await notifyHub.Clients.Group(inputDto.ReportBy).SendAsync("notification");

                    //await notifyHub.Clients.Group(sellerId).SendAsync("notification");
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
