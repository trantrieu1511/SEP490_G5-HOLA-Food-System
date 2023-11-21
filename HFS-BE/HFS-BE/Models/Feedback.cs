using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            FeedBackImages = new HashSet<FeedBackImage>();
            FeedbackReplies = new HashSet<FeedbackReply>();
            FeedbackVotes = new HashSet<FeedbackVote>();
        }

        public int FeedbackId { get; set; }
        public int? FoodId { get; set; }
        public string? CustomerId { get; set; }
        public string? FeedbackMessage { get; set; }
        public byte? Star { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte? Status { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Food? Food { get; set; }
        public virtual ICollection<FeedBackImage> FeedBackImages { get; set; }
        public virtual ICollection<FeedbackReply> FeedbackReplies { get; set; }
        public virtual ICollection<FeedbackVote> FeedbackVotes { get; set; }
    }
}
