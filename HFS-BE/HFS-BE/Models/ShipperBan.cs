using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class ShipperBan
    {
        public int BanShipperId { get; set; }
        public string ShipperId { get; set; } = null!;
        public string? Reason { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Shipper Shipper { get; set; } = null!;
    }
}
