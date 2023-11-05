using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class SellerBan
    {
        public int BanSellerId { get; set; }
        public string SellerId { get; set; } = null!;
        public string? Reason { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Seller Seller { get; set; } = null!;
    }
}
