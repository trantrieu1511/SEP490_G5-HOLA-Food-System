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
    public class GetAllFoodReportController : BaseController
    {
        public GetAllFoodReportController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("menureports/getallfoodreports")]
        [Authorize]
        public ListFoodReportOutputDto GetAllFoodReports()
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("MM") && !userRole.Equals("CU"))
                {
                    return Output<ListFoodReportOutputDto>(Constants.ResultCdFail, "Please login as a menu moderator or customer before executing this API.");
                }
                var business = GetBusinessLogic<GetAllFoodReportBusinessLogic>();
                return business.GetAllFoodReports(GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<ListFoodReportOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
