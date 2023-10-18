using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class FeedbackReply
    {
        public int ReplyId { get; set; }
        public int FeedbackId { get; set; }
        public string? ReplyMessage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte? Status { get; set; }

        public virtual Feedback Feedback { get; set; } = null!;
    }
}
