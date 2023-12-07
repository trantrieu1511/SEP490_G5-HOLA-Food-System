using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Dashboard;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Dashboard
{
    public class DashboardSellerController : BaseController
    {
        public DashboardSellerController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet]
        [Route("dashboards/dashboardSeller")]
        [Authorize]
        public DashboardSellerOutput DashboardSeller([FromQuery] DashboardInputDto inputDto)
        {
            try
            {
                var busi = GetBusinessLogic<DashboardSellerBusinessLogic>();
                return busi.DashboardSeller(new Dao.OrderDao.DashboardInputDaoDto
                {
                    DateFrom = inputDto.DateFrom,
                    DateEnd = inputDto.DateEnd,
                    SellerId = GetUserInfor().UserId
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
