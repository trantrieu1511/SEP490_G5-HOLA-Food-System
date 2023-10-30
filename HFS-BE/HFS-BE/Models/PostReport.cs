using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class PostReport
    {
        public int PostId { get; set; }
        public string ReportBy { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
        public bool IsDone { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual Customer ReportByNavigation { get; set; } = null!;
        public virtual PostModerator? UpdateByNavigation { get; set; }
    }
}
