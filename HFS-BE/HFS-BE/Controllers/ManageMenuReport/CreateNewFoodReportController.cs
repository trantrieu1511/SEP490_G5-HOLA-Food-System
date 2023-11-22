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
    public class CreateNewFoodReportController : BaseControllerSignalR
    {
        public CreateNewFoodReportController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("menureports/createnewfoodreport")]
        [Authorize]
        public async Task<BaseOutputDto> CreateNewFoodReport(CreateNewFoodReportInputDto inputDto)
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("CU"))
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a customer before executing this API.");
                }
                var business = GetBusinessLogic<CreateNewFoodReportBusinessLogic>();

                var output = business.CreateNewFoodReport(inputDto, GetUserInfor().UserId);

                // call signalR to Food Modelrator
                if (output.Success)
                {
                    //notify for all Food Modelrator
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();

                    await notifyHub.Clients.All.SendAsync("foodNotification");
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
