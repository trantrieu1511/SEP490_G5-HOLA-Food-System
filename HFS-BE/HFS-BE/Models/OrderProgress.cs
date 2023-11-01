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
        public string? SellerId { get; set; }
        public string? CustomerId { get; set; }
        public string? ShipperId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual Shipper? Shipper { get; set; }
    }
}
