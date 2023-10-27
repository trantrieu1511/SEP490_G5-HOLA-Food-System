using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class PostModerator
    {
        public PostModerator()
        {
            NotificationReceiver3s = new HashSet<Notification>();
            NotificationSendBy3s = new HashSet<Notification>();
            PostReports = new HashSet<PostReport>();
        }

        public string ModId { get; set; } = null!;
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
        public bool ConfirmEmail { get; set; }

        public virtual ICollection<Notification> NotificationReceiver3s { get; set; }
        public virtual ICollection<Notification> NotificationSendBy3s { get; set; }
        public virtual ICollection<PostReport> PostReports { get; set; }
    }
}
