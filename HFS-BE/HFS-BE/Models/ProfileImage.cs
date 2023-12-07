using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class ProfileImage
    {
        public int ImageId { get; set; }
        public string UserId { get; set; } = null!;
        public string Path { get; set; } = null!;
        public bool IsReplaced { get; set; }
    }
}
