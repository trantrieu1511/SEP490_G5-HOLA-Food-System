using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageVoucher;
using HFS_BE.BusinessLogic.ShopDetail;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ShopDetail
{
    public class GetVoucherController : BaseController
    {
        public GetVoucherController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("shopdetail/voucher")]
        public GetListVoucherDaoOutputDto GetVoucher(GetListVoucherDaoInput input)
        {
            try
            {
                var business = this.GetBusinessLogic<GetShopVoucherBusinessLogic>();

                var output = business.GetListVoucher(input);

                return output;
            }
            catch (Exception)
            {
                return this.Output<GetListVoucherDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
