using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Admin
    {
        public Admin()
        {
            SellerReports = new HashSet<SellerReport>();
        }

        public string AdminId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public bool IsOnline { get; set; }
        public decimal? WalletBalance { get; set; }
        public bool? ConfirmedEmail { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<SellerReport> SellerReports { get; set; }
    }
}
