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

    public class UpdateVoucherController : BaseController
    {
        public UpdateVoucherController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("vouchers/updateVoucher")]
        //[Authorize]

        public BaseOutputDto UpdateVoucher(UpdateVoucherDaoInput input)
        {
            try
            {

                var business = this.GetBusinessLogic<UpdateVoucherBusinessLogic>();

                var output = business.UpdateVoucher(input);

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
