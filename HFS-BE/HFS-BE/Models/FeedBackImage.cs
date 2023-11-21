using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class FeedBackImage
    {
        public int ImagefeedbackId { get; set; }
        public int? FeedbackId { get; set; }
        public string? Path { get; set; }

        public virtual Feedback? Feedback { get; set; }
    }
}
