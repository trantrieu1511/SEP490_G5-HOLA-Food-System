using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Transaction;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.TransactionWallet
{
    public class GetDashboardInfoController : BaseController
    {
        public GetDashboardInfoController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("wallet/dashboard")]
        [Authorize]
        public DashboardAccountantOutputDto GetDashboard(DashboardAccountantInputDto inputDto)
        {
            try
            {
                var userInfo = this.GetUserInfor();
                if (!userInfo.Role.Equals("AC"))
                {
                    return this.Output<DashboardAccountantOutputDto>(Constants.ResultCdFail, "Unauthorized");
                }
                var busi = this.GetBusinessLogic<DashboardAccountantBusinessLogic>();
                return busi.GetDashBoard(inputDto);
            }
            catch (Exception)
            {
                return this.Output<DashboardAccountantOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
