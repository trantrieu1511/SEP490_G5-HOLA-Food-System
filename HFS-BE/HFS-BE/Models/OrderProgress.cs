using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class OrderProgress
    {
        public int OrderProgressId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Image { get; set; }
        public int? OrderId { get; set; }
        public byte? Status { get; set; }
        public int? UserId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual User? User { get; set; }
    }
}
