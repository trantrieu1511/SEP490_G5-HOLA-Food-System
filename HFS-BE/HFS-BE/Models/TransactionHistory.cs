using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class TransactionHistory
    {
        public int TransactionId { get; set; }
        public string SenderId { get; set; } = null!;
        public string? RecieverId { get; set; }
        public string TransactionType { get; set; } = null!;
        public string? Note { get; set; }
        public decimal Value { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte? Status { get; set; }
        public string? AcceptBy { get; set; }

        public virtual Accountant? AcceptByNavigation { get; set; }
    }
}
