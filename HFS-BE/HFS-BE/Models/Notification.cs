﻿using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int Lang { get; set; }
        public string? SendBy { get; set; }
        public string? Receiver { get; set; }
        public int Type { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsRead { get; set; }
    }
}
