using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            FeedbackReplies = new HashSet<FeedbackReply>();
            FeedbackVotes = new HashSet<FeedbackVote>();
        }

        public int FeedbackId { get; set; }
        public int? FoodId { get; set; }
        public int? UserId { get; set; }
        public string? FeedbackMessage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? Status { get; set; }

        public virtual Food? Food { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<FeedbackReply> FeedbackReplies { get; set; }
        public virtual ICollection<FeedbackVote> FeedbackVotes { get; set; }
    }
}
