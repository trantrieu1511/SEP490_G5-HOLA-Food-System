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
    public class GetListVoucherController : BaseController
    {
        public GetListVoucherController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("vouchers/getListvoucher")]
        //[Authorize]
        public GetListVoucherDaoOutputDto GetVoucher(GetListVoucherDaoInput input)
        {
            try
            {
                var business = this.GetBusinessLogic<GetVoucherBusinessLogic>();

                var output = business.GetListVoucher(input);

                // call signalR to Post Modelrator

                return output;
            }
            catch (Exception)
            {
                return this.Output<GetListVoucherDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
