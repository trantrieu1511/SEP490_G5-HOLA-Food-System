using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class SellerLicenseImage
    {
        public int ImageLicenseId { get; set; }
        public string SellerId { get; set; } = null!;
        public string? Path { get; set; }
        public bool IsReplaced { get; set; }

        public virtual Seller Seller { get; set; } = null!;
    }
}
