using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string CustomerId { get; set; } = null!;
        public string? CommentContent { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
