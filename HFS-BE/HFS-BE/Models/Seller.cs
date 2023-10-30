using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Seller
    {
        public Seller()
        {
            CartItems = new HashSet<CartItem>();
            FeedbackReplies = new HashSet<FeedbackReply>();
            Foods = new HashSet<Food>();
            NotificationReceiver4s = new HashSet<Notification>();
            NotificationSendBy4s = new HashSet<Notification>();
            OrderProgresses = new HashSet<OrderProgress>();
            Orders = new HashSet<Order>();
            Posts = new HashSet<Post>();
            Shippers = new HashSet<Shipper>();
            Vouchers = new HashSet<Voucher>();
        }

        public string SellerId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public long? PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public string? Avatar { get; set; }
        public bool IsOnline { get; set; }
        public decimal? WalletBalance { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public int? ManageBy { get; set; }
        public bool ConfirmEmail { get; set; }
        public bool? Ban { get; set; }
        public bool? CheckSeller { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<FeedbackReply> FeedbackReplies { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
        public virtual ICollection<Notification> NotificationReceiver4s { get; set; }
        public virtual ICollection<Notification> NotificationSendBy4s { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Shipper> Shippers { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
