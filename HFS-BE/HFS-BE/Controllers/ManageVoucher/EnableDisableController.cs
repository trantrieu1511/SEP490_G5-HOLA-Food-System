using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageVoucher;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageVoucher
{
    public class EnableDisableController : BaseController
    {
        public EnableDisableController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("vouchers/Enable_Disable")]
        [Authorize(Roles = "SE")]

        public BaseOutputDto EnableDisableVoucher(Enable_Disable_VoucherDaoInput input)
        {
            try
            {

                var business = this.GetBusinessLogic<EnableDisableBusinessLogic>();

                var output = business.EnableDisableVoucher(input);

                // call signalR to Post Modelrator

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
