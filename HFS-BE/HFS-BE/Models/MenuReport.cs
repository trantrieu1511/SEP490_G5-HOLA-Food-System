using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class MenuReport
    {
        public int FoodId { get; set; }
        public int UserId { get; set; }
        public string ReportContent { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDone { get; set; }

        public virtual Food Food { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
