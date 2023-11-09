using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageMenuReport;
using HFS_BE.DAO.MenuReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageMenuReport
{
    public class ApproveNotApproveMenuReportController : BaseController
    {
        public ApproveNotApproveMenuReportController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("menureports/approvenotapprovefoodreport")]
        [Authorize]
        public BaseOutputDto ApproveNotApproveFoodReport(ApproveNotApproveFoodReportInputDto inputDto)
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("MM"))
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a menu moderator before executing this API.");
                }
                var business = GetBusinessLogic<ApproveNotApproveFoodReportBusinessLogic>();
                return business.ApproveNotApproveFoodReport(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
