﻿using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            OrderProgresses = new HashSet<OrderProgress>();
            Orders = new HashSet<Order>();
            ShipperBans = new HashSet<ShipperBan>();
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
        public bool? IsBanned { get; set; }
        public bool? IsVerified { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual Seller? ManageByNavigation { get; set; }
        public virtual ICollection<OrderProgress> OrderProgresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShipperBan> ShipperBans { get; set; }
    }
}
