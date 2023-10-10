using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual NotificationType Type { get; set; } = null!;
    }
}
