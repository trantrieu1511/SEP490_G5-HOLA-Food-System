using HFS_BE.Base;
using HFS_BE.Utils;

namespace HFS_BE.DAO.VoucherDao
{
    public class CreateVoucherDaoInputDto
    {
        public string? SellerId { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal? MinimumOrderValue { get; set; }
        public byte? Status { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }

    public class GetListVoucherDaoInput
    {
        public string SellerId { get; set; }
    }
    public class UpdateVoucherDaoInput
    {
        public int VoucherId { get; set; }
        public string Code { get; set; } = null!;
        public decimal DiscountAmount { get; set; }
        public decimal? MinimumOrderValue { get; set; }
        public byte? Status { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
