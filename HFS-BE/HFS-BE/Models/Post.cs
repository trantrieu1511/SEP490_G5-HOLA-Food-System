using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            PostImages = new HashSet<PostImage>();
            PostReports = new HashSet<PostReport>();
        }

        public int PostId { get; set; }
        public string SellerId { get; set; } = null!;
        public string? PostContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte? Status { get; set; }
        public string? BanBy { get; set; }
        public DateTime? BanDate { get; set; }

        public virtual PostModerator? BanByNavigation { get; set; }
        public virtual Seller Seller { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
    }
}
