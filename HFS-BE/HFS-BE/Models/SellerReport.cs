using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class SellerReport
    {
        public SellerReport()
        {
            SellerReportImages = new HashSet<SellerReportImage>();
        }

        public int SellerReportId { get; set; }
        public string SellerId { get; set; } = null!;
        public string ReportBy { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
        public byte Status { get; set; }
        public string? Note { get; set; }

        public virtual Customer ReportByNavigation { get; set; } = null!;
        public virtual Seller Seller { get; set; } = null!;
        public virtual Admin? UpdateByNavigation { get; set; }
        public virtual ICollection<SellerReportImage> SellerReportImages { get; set; }
    }
}
