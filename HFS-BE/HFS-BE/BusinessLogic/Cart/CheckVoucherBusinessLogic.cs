using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.ChatMessageDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Cart
{
    public class CheckVoucherBusinessLogic : BaseBusinessLogic
    {
        public CheckVoucherBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public VoucherDetailOutput CheckVoucer(string voucherCd, string userId)
        {
            try
            {
                var now = DateTime.Now;
                var dao = this.CreateDao<VoucherDao>();
                var output = this.Output<VoucherDetailOutput>(Constants.ResultCdSuccess);
                var voucher = dao.GetVoucherByCode(voucherCd);
                if (voucher == null)
                {
                    return this.Output<VoucherDetailOutput>(Constants.ResultCdFail, "Voucher not Exsit!");
                }

                output.isEffective = voucher.EffectiveDate > now ? false : true;
                output.isExpired = voucher.ExpireDate < now ? true : false;
                output.MinValue = voucher.MinimumOrderValue;
                output.SellerId = voucher.SellerId;
                output.Discount = voucher.DiscountAmount;

                var isUsed = dao.CheckUsedVoucher(userId, voucher.VoucherId);
                output.isUsed = isUsed;

                return output;
            }
            catch (Exception)
            {
                return this.Output<VoucherDetailOutput>(Constants.ResultCdFail);
            }
        }
    }
}
