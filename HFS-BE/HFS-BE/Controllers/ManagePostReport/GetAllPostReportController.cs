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
    public class GetAllPostReportController : BaseController
    {
        public GetAllPostReportController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("postreports/getallpostreports")]
        [Authorize]
        public ListPostReportOutputDto GetAllPostReports()
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("PM") && !userRole.Equals("CU"))
                {
                    return Output<ListPostReportOutputDto>(Constants.ResultCdFail, "Please login as a post moderator or customer before executing this API.");
                }
                var business = GetBusinessLogic<GetAllPostReportBusinessLogic>();
                return business.GetAllPostReports(GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<ListPostReportOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
