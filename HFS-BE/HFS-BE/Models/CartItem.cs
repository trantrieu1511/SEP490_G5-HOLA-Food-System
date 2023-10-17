using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class CartItem
    {
        public int FoodId { get; set; }
        public int CartId { get; set; }
        public int Amount { get; set; }

        public virtual User Cart { get; set; } = null!;
        public virtual Food Food { get; set; } = null!;
    }
}
