using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string? SendBy { get; set; }
        public string? Receiver { get; set; }
        public int TypeId { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Customer? Receiver1 { get; set; }
        public virtual MenuModerator? Receiver2 { get; set; }
        public virtual PostModerator? Receiver3 { get; set; }
        public virtual Seller? Receiver4 { get; set; }
        public virtual Shipper? Receiver5 { get; set; }
        public virtual Admin? ReceiverNavigation { get; set; }
        public virtual Customer? SendBy1 { get; set; }
        public virtual MenuModerator? SendBy2 { get; set; }
        public virtual PostModerator? SendBy3 { get; set; }
        public virtual Seller? SendBy4 { get; set; }
        public virtual Shipper? SendBy5 { get; set; }
        public virtual Admin? SendByNavigation { get; set; }
        public virtual NotificationType Type { get; set; } = null!;
    }
}
