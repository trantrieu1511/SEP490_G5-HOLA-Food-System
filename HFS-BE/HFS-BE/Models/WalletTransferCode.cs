using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class WalletTransferCode
    {
        public int CodeId { get; set; }
        public string? UserId { get; set; }
        public string? Code { get; set; }
        public bool? IsUsed { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
