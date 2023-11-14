using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class ProfileImage
    {
        public int ImageId { get; set; }
        public int? UserId { get; set; }
        public string? Path { get; set; }
    }
}
