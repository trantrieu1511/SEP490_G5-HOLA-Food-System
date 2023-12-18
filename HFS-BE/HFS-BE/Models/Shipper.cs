using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            Invitations = new HashSet<Invitation>();
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
        public bool? IsOnline { get; set; }
        public string? ManageBy { get; set; }
        public bool? ConfirmedEmail { get; set; }
        public byte Status { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Note { get; set; }
        public bool? IsPhoneVerified { get; set; }
        public string? OtpToken { get; set; }
        public int? OtpTokenExpiryTime { get; set; }
        public string? IdcardNumber { get; set; }
        public string? IdcardFrontImage { get; set; }
        public string? IdcardBackImage { get; set; }

        public virtual Seller? ManageByNavigation { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
