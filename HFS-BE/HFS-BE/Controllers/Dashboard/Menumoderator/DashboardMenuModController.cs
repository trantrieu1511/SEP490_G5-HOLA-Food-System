using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Dashboard.Menumoderator;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Dashboard.Menumoderator
{
    public class DashboardMenuModController : BaseController
    {
        public DashboardMenuModController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("dashboards/getAllTimeStatisticsMenuModerator")]
        [Authorize]
        public DashboardMenuModStatisticOutput GetAllTimeStatistics()
        {
            try
            {
                var business = GetBusinessLogic<DashboardMenumodBusinessLogic>();
                return business.GetAllTimeStatistics(GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<DashboardMenuModStatisticOutput>(Constants.ResultCdFail);
            }
        }

        [HttpGet("dashboards/getMyStatisticsMenumod")]
        [Authorize]
        public DashboardMenumodOutput GetMyStatisticsFromDateRange([FromQuery] DashboardInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<DashboardMenumodBusinessLogic>();
                return business.GetMyStatisticsFromDateRange(new DAO.MenuReportDao.DashboardMenuModInputDto
                {
                    DateFrom = inputDto.DateFrom,
                    DateEnd = inputDto.DateEnd,
                    ModId = GetUserInfor().UserId
                });
            }
            catch (Exception)
            {
                return Output<DashboardMenumodOutput>(Constants.ResultCdFail);
            }
        }

        [HttpGet("dashboards/getSystemStatisticsMenumod")]
        [Authorize]
        public DashboardMenumodOutput GetSystemStatisticsFromDateRange([FromQuery] DashboardInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<DashboardMenumodBusinessLogic>();
                return business.GetSystemStatisticsFromDateRange(new DAO.MenuReportDao.DashboardMenuModInputDto
                {
                    DateFrom = inputDto.DateFrom,
                    DateEnd = inputDto.DateEnd,
                    ModId = GetUserInfor().UserId
                });
            }
            catch (Exception)
            {
                return Output<DashboardMenumodOutput>(Constants.ResultCdFail);
            }
        }
    }
}
