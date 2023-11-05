using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class PostModerator
    {
        public PostModerator()
        {
            PostReports = new HashSet<PostReport>();
        }

        public string ModId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public byte[] PasswordSalt { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public string? Avatar { get; set; }
        public bool IsOnline { get; set; }
        public bool? ConfirmedEmail { get; set; }
        public bool? IsBanned { get; set; }

        public virtual ICollection<PostReport> PostReports { get; set; }
    }
}
