using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Chat
    {
        public int ChatId { get; set; }
        public string SenderId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime SentAt { get; set; }

        public virtual Customer Receiver { get; set; } = null!;
        public virtual Seller ReceiverNavigation { get; set; } = null!;
        public virtual Customer Sender { get; set; } = null!;
        public virtual Seller SenderNavigation { get; set; } = null!;
    }
}
