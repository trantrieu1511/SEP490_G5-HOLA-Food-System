using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class ShipAddress
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string AddressInfo { get; set; } = null!;
        public bool? IsDefaultAddress { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
