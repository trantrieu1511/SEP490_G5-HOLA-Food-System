using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class CustomerBan
    {
        public int BanCustomerId { get; set; }
        public string CustomerId { get; set; } = null!;
        public string? Reason { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
