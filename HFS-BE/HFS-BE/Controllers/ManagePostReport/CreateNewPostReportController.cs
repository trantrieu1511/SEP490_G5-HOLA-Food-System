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
    public class CreateNewPostReportController : BaseController
    {
        public CreateNewPostReportController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("postreports/createnewpostreport")]
        [Authorize]
        public BaseOutputDto CreateNewPostReport(CreateNewPostReportInputDto inputDto)
        {
            try
            {
                string userRole = GetUserInfor().UserId.Substring(0, 2);
                if (!userRole.Equals("CU"))
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a customer before executing this API.");
                }
                var business = GetBusinessLogic<CreateNewPostReportBusinessLogic>();
                return business.CreateNewPostReport(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
