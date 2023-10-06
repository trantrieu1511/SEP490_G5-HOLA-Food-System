using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class PostImage
    {
        public int? PostId { get; set; }
        public string? Path { get; set; }

        public virtual Post? Post { get; set; }
    }
}
