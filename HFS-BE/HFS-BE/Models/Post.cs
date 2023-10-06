using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? PostContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
