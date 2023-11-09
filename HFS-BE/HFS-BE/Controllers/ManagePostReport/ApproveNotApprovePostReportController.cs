using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePostReport;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePostReport
{
    public class ApproveNotApprovePostReportController : BaseController
    {
        public ApproveNotApprovePostReportController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("postreports/approvenotapprovepostreport")]
        [Authorize]
        public BaseOutputDto ApproveNotApprovePostReport(ApproveNotApprovePostReportInputDto inputDto)
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("PM"))
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a post moderator before executing this API.");
                }
                var business = GetBusinessLogic<ApproveNotApprovePostReportBusinessLogic>();
                return business.ApproveNotApprovePostReport(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
