using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Group
    {
        public Group()
        {
            Connections = new HashSet<Connection>();
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<Connection> Connections { get; set; }
    }
}
