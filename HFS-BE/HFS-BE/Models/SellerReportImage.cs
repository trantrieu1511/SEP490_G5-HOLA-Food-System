using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class SellerReportImage
    {
        public int ImageSellerReportId { get; set; }
        public int? SellerReportId { get; set; }
        public string? Path { get; set; }

        public virtual SellerReport? SellerReport { get; set; }
    }
}
