using HFS_BE.Base;

namespace HFS_BE.DAO.VoucherDao
{
    public class CreateVoucherDaoOutputDto:BaseOutputDto
    {
        public int VoucherId { get; set; }
        public string? SellerId { get; set; }
        public string Code { get; set; } = null!;
        public decimal DiscountAmount { get; set; }
        public decimal? MinimumOrderValue { get; set; }
        public byte? Status { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }

    public class GetVoucherDaoOutputDto
    {
        public int VoucherId { get; set; }
        public string? SellerId { get; set; }
        public string Code { get; set; } = null!;
        public decimal DiscountAmount { get; set; }
        public decimal? MinimumOrderValue { get; set; }
        public byte? Status { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }

    public class GetListVoucherDaoOutputDto : BaseOutputDto
    { 
        public List<GetVoucherDaoOutputDto> listVoucher { get; set; }
    }

    public class UpadteVoucherDaoOutputDto : BaseOutputDto
    {
        public GetVoucherDaoOutputDto Voucher { get; set; }
    }
}
