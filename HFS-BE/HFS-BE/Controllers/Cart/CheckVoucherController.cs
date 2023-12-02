using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Cart
{
    public class CheckVoucherController : BaseController
    {
        public CheckVoucherController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("checkout/checkvoucher")]
        [Authorize]
        public VoucherDetailOutput CheckVoucher(GetVoucherInputDto inputDto)
        {
            try
            {
                var userInfo = this.GetUserInfor();
                var busi = this.GetBusinessLogic<CheckVoucherBusinessLogic>();
                return busi.CheckVoucer(inputDto.Voucher, userInfo.UserId);
            }
            catch (Exception)
            {
                return this.Output<VoucherDetailOutput>(Constants.ResultCdFail);
            }
        }

        public class GetVoucherInputDto
        {
            public string Voucher { get; set; }
        }
    }
}
