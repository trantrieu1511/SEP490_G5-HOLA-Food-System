﻿using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class FeedbackVote
    {
        public int VoteId { get; set; }
        public int FeedbackId { get; set; }
        public bool? IsLike { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string VoteBy { get; set; } = null!;

        public virtual Feedback Feedback { get; set; } = null!;
        public virtual Customer VoteByNavigation { get; set; } = null!;
    }
}
