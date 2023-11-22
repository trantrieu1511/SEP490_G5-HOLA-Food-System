using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            OrderProgresses = new HashSet<OrderProgress>();
        }

        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? SellerId { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipperId { get; set; }
        public int? VoucherId { get; set; }
        public byte? Status { get; set; }
        public byte? PaymentMethod { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual Shipper? Shipper { get; set; }
        public virtual Voucher? Voucher { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
    }
}
