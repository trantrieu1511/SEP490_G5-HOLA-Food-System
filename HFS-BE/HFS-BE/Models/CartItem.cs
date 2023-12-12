using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class CartItem
    {
        public int FoodId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CartId { get; set; } = null!;
        public int Amount { get; set; }

        public virtual Customer Cart { get; set; } = null!;
        public virtual Food Food { get; set; } = null!;
    }
}
