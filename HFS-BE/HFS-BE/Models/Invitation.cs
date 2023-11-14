using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Invitation
    {
        public string SellerId { get; set; } = null!;
        public string ShipperId { get; set; } = null!;
        public byte Accepted { get; set; }

        public virtual Seller Seller { get; set; } = null!;
        public virtual Shipper Shipper { get; set; } = null!;
    }
}
