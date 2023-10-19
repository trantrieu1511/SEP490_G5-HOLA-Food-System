using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class FoodImage
    {
        public int ImageId { get; set; }
        public int? FoodId { get; set; }
        public string? Path { get; set; }
        public string? PublicId { get; set; }

        public virtual Food? Food { get; set; }
    }
}
