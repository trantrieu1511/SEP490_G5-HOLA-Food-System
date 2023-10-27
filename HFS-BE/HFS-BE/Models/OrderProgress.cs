using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class OrderProgress
    {
        public int OrderProgressId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Image { get; set; }
        public int? OrderId { get; set; }
        public byte? Status { get; set; }
        public string? CreatedBy { get; set; }

        public virtual Seller? CreatedBy1 { get; set; }
        public virtual Shipper? CreatedBy2 { get; set; }
        public virtual Customer? CreatedByNavigation { get; set; }
        public virtual Order? Order { get; set; }
    }
}
