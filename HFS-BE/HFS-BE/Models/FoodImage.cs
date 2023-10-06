using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class FoodImage
    {
        public int? FoodId { get; set; }
        public string? Path { get; set; }

        public virtual Food? Food { get; set; }
    }
}
