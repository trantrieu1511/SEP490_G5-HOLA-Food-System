using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Seller
    {
        public Seller()
        {
            ChatMessages = new HashSet<ChatMessage>();
            FeedbackReplies = new HashSet<FeedbackReply>();
            Foods = new HashSet<Food>();
            Invitations = new HashSet<Invitation>();
            OrderProgresses = new HashSet<OrderProgress>();
            Orders = new HashSet<Order>();
            Posts = new HashSet<Post>();
            SellerBans = new HashSet<SellerBan>();
            SellerLicenseImages = new HashSet<SellerLicenseImage>();
            SellerReports = new HashSet<SellerReport>();
            Shippers = new HashSet<Shipper>();
            Vouchers = new HashSet<Voucher>();
        }

        public string SellerId { get; set; } = null!;
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public bool IsOnline { get; set; }
        public decimal? WalletBalance { get; set; }
        public bool? ConfirmedEmail { get; set; }
        public bool? IsBanned { get; set; }
        public bool? IsVerified { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<FeedbackReply> FeedbackReplies { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<SellerBan> SellerBans { get; set; }
        public virtual ICollection<SellerLicenseImage> SellerLicenseImages { get; set; }
        public virtual ICollection<SellerReport> SellerReports { get; set; }
        public virtual ICollection<Shipper> Shippers { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
