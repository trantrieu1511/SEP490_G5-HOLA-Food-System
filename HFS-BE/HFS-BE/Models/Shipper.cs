using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            NotificationReceiver5s = new HashSet<Notification>();
            NotificationSendBy5s = new HashSet<Notification>();
            OrderProgresses = new HashSet<OrderProgress>();
            Orders = new HashSet<Order>();
        }

        public string ShipperId { get; set; } = null!;
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
        public string? ManageBy { get; set; }
        public bool ConfirmEmail { get; set; }
        public bool? Ban { get; set; }
        public bool? IsVerify { get; set; }

        public virtual Seller? ManageByNavigation { get; set; }
        public virtual ICollection<Notification> NotificationReceiver5s { get; set; }
        public virtual ICollection<Notification> NotificationSendBy5s { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
