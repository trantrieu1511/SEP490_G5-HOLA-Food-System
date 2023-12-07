using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class MenuReport
    {
        public int FoodId { get; set; }
        public string ReportBy { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public string? UpdateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte Status { get; set; }
        public string? Note { get; set; }

        public virtual Food Food { get; set; } = null!;
        public virtual Customer ReportByNavigation { get; set; } = null!;
        public virtual MenuModerator? UpdateByNavigation { get; set; }
    }
}
