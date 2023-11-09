using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class ChatMessage
    {
        public int MessageId { get; set; }
        public string CustomerId { get; set; } = null!;
        public string SellerId { get; set; } = null!;
        public bool SenderType { get; set; }
        public string Message { get; set; } = null!;
        public DateTime SentAt { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Seller Seller { get; set; } = null!;
    }
}
