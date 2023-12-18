using HFS_BE.Base;
using HFS_BE.Utils;
using HFS_BE.Utils.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.DAO.VoucherDao
{
    public class CreateVoucherDaoInputDto
    {
        [Required]
        public string? SellerId { get; set; }
        [Required]
        public decimal DiscountAmount { get; set; }
        [Required]
        public decimal? MinimumOrderValue { get; set; }
        [Required]
        [DateNotInPast(ErrorMessage = "EffectiveDate cannot be in the past")]
        public DateTime EffectiveDate { get; set; }
        [Required]
        [DateEndAfterDateFrom(ErrorMessage = "ExpireDate must be greater than or equal to EffectiveDate")]
        public DateTime ExpireDate { get; set; }
    }

    public class GetListVoucherDaoInput
    {
        public string SellerId { get; set; }
    }
    public class UpdateVoucherDaoInput
    {
        public int VoucherId { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal? MinimumOrderValue { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
    public class Enable_Disable_VoucherDaoInput
    {
        public int VoucherId { get; set; }
        public bool Type { get; set; }
    }

}
