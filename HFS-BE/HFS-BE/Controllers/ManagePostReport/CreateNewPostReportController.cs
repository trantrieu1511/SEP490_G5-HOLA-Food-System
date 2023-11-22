using AutoMapper;
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
    public class CreateNewPostReportController : BaseControllerSignalR
    {
        public CreateNewPostReportController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("postreports/createnewpostreport")]
        [Authorize]
        public async Task<BaseOutputDto> CreateNewPostReport(CreateNewPostReportInputDto inputDto)
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("CU"))
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a customer before executing this API.");
                }
                var business = GetBusinessLogic<CreateNewPostReportBusinessLogic>();

                var output = business.CreateNewPostReport(inputDto, GetUserInfor().UserId);

                // call signalR to Post Modelrator
                if (output.Success)
                {
                    //notify for all Post Modelrator
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();

                    await notifyHub.Clients.All.SendAsync("postNotification");
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
