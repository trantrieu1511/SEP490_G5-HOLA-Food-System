using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Connection
    {
        public string ConnectionId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string GroupName { get; set; } = null!;

        public virtual Group GroupNameNavigation { get; set; } = null!;
    }
}
