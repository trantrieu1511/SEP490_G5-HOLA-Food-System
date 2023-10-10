using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class NotificationType
    {
        public NotificationType()
        {
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
