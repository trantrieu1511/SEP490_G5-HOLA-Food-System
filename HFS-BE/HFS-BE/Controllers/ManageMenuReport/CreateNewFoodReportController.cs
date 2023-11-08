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
    public class CreateNewFoodReportController : BaseController
    {
        public CreateNewFoodReportController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("menureports/createnewfoodreport")]
        [Authorize]
        public BaseOutputDto CreateNewFoodReport(CreateNewFoodReportInputDto inputDto)
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("CU"))
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a customer before executing this API.");
                }
                var business = GetBusinessLogic<CreateNewFoodReportBusinessLogic>();
                return business.CreateNewFoodReport(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
