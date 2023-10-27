using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            Orders = new HashSet<Order>();
        }

        public int VoucherId { get; set; }
        public string? SellerId { get; set; }
        public string? VoucherName { get; set; }
        public byte? Status { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        public virtual Seller? Seller { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
