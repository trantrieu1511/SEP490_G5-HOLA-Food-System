using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class User
    {
        public User()
        {
            Feedbacks = new HashSet<Feedback>();
            InverseManageByNavigation = new HashSet<User>();
            MenuReports = new HashSet<MenuReport>();
            OrderCustomers = new HashSet<Order>();
            OrderProgresses = new HashSet<OrderProgress>();
            OrderShippers = new HashSet<Order>();
            PostReports = new HashSet<PostReport>();
            Posts = new HashSet<Post>();
            ShipAddresses = new HashSet<ShipAddress>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = null!;
        public long? PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public string? Avatar { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public bool IsOnline { get; set; }
        public decimal? WalletBalance { get; set; }
        public int? ManageBy { get; set; }

        public virtual User? ManageByNavigation { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<User> InverseManageByNavigation { get; set; }
        public virtual ICollection<MenuReport> MenuReports { get; set; }
        public virtual ICollection<Order> OrderCustomers { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
        public virtual ICollection<Order> OrderShippers { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<ShipAddress> ShipAddresses { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
