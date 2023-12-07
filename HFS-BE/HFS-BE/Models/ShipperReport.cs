using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class ShipperReport
    {
        public string ShipperId { get; set; } = null!;
        public string ReportBy { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
        public byte Status { get; set; }
        public string? Note { get; set; }

        public virtual Seller ReportByNavigation { get; set; } = null!;
        public virtual Shipper Shipper { get; set; } = null!;
        public virtual Admin? UpdateByNavigation { get; set; }
    }
}
