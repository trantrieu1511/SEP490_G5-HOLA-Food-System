using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public byte? Status { get; set; }

        public virtual Food Food { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
