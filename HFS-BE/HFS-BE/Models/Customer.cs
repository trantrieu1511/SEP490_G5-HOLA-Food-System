using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CartItems = new HashSet<CartItem>();
            ChatMessages = new HashSet<ChatMessage>();
            Comments = new HashSet<Comment>();
            FeedbackReplies = new HashSet<FeedbackReply>();
            FeedbackVotes = new HashSet<FeedbackVote>();
            Feedbacks = new HashSet<Feedback>();
            MenuReports = new HashSet<MenuReport>();
            OrderProgresses = new HashSet<OrderProgress>();
            Orders = new HashSet<Order>();
            PostReports = new HashSet<PostReport>();
            PostVotes = new HashSet<PostVote>();
            SellerReports = new HashSet<SellerReport>();
            ShipAddresses = new HashSet<ShipAddress>();
        }

        public string CustomerId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public bool? IsOnline { get; set; }
        public decimal? WalletBalance { get; set; }
        public bool? ConfirmedEmail { get; set; }
        public int NumberOfViolations { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsPhoneVerified { get; set; }
        public string? OtpToken { get; set; }
        public int? OtpTokenExpiryTime { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FeedbackReply> FeedbackReplies { get; set; }
        public virtual ICollection<FeedbackVote> FeedbackVotes { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<MenuReport> MenuReports { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
        public virtual ICollection<PostVote> PostVotes { get; set; }
        public virtual ICollection<SellerReport> SellerReports { get; set; }
        public virtual ICollection<ShipAddress> ShipAddresses { get; set; }
    }
}
