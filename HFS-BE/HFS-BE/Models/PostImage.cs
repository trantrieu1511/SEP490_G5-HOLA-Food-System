using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class PostImage
    {
        public int ImageId { get; set; }
        public int? PostId { get; set; }
        public string? Path { get; set; }
        public string? PublicId { get; set; }

        public virtual Post? Post { get; set; }
    }
}
