﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Dashboard.Postmoderator;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Dashboard.Postmoderator
{
    public class DashboardPostModController : BaseController
    {
        public DashboardPostModController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("dashboards/getalltimestatisticspostmoderator")]
        [Authorize]
        public DashboardPostModStatisticOutput GetAllTimeStatistics()
        {
            try
            {
                var business = GetBusinessLogic<DashboardPostmodBusinessLogic>();
                return business.GetAllTimeStatistics(GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<DashboardPostModStatisticOutput>(Constants.ResultCdFail);
            }
        }

        [HttpGet("dashboards/getMyStatisticsPostmod")]
        [Authorize]
        public DashboardPostmodOutput GetMyStatisticsFromDateRange([FromQuery] DashboardInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<DashboardPostmodBusinessLogic>();
                return business.GetMyStatisticsFromDateRange(new DAO.PostReportDao.DashboardPostModInputDto
                {
                    DateFrom = inputDto.DateFrom,
                    DateEnd = inputDto.DateEnd,
                    ModId = GetUserInfor().UserId
                });
            }
            catch (Exception)
            {
                return Output<DashboardPostmodOutput>(Constants.ResultCdFail);
            }
        }
        
        [HttpGet("dashboards/getSystemStatisticsPostmod")]
        [Authorize]
        public DashboardPostmodOutput GetSystemStatisticsFromDateRange([FromQuery] DashboardInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<DashboardPostmodBusinessLogic>();
                return business.GetSystemStatisticsFromDateRange(new DAO.PostReportDao.DashboardPostModInputDto
                {
                    DateFrom = inputDto.DateFrom,
                    DateEnd = inputDto.DateEnd,
                    ModId = GetUserInfor().UserId
                });
            }
            catch (Exception)
            {
                return Output<DashboardPostmodOutput>(Constants.ResultCdFail);
            }
        }

        //[HttpGet("dashboards/getthismonthstatisticspostmoderator")]
        //[Authorize]
        //public DashboardPostModStatisticOutput GetThisMonthStatistics()
        //{
        //    try
        //    {
        //        var business = GetBusinessLogic<DashboardPostmodBusinessLogic>();
        //        return business.GetThisMonthStatistics(GetUserInfor().UserId);
        //    }
        //    catch (Exception)
        //    {
        //        return Output<DashboardPostModStatisticOutput>(Constants.ResultCdFail);
        //    }
        //}
    }
}
