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
            CustomerBans = new HashSet<CustomerBan>();
            FeedbackReplies = new HashSet<FeedbackReply>();
            FeedbackVotes = new HashSet<FeedbackVote>();
            Feedbacks = new HashSet<Feedback>();
            MenuReports = new HashSet<MenuReport>();
            OrderProgresses = new HashSet<OrderProgress>();
            Orders = new HashSet<Order>();
            PostReports = new HashSet<PostReport>();
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
        public string? Avatar { get; set; }
        public bool? IsOnline { get; set; }
        public decimal? WalletBalance { get; set; }
        public bool? ConfirmedEmail { get; set; }
        public bool? IsBanned { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<CustomerBan> CustomerBans { get; set; }
        public virtual ICollection<FeedbackReply> FeedbackReplies { get; set; }
        public virtual ICollection<FeedbackVote> FeedbackVotes { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<MenuReport> MenuReports { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
        public virtual ICollection<ShipAddress> ShipAddresses { get; set; }
    }
}
