using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Post
    {
        public Post()
        {
            PostImages = new HashSet<PostImage>();
            PostReports = new HashSet<PostReport>();
        }

        public int PostId { get; set; }
        public int ShopId { get; set; }
        public string? PostContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte? Status { get; set; }

        public virtual User Shop { get; set; } = null!;
        public virtual ICollection<PostImage> PostImages { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
    }
}
