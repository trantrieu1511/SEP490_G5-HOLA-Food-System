using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class ShipAddress
    {
        public int AddressId { get; set; }
        public string CustomerId { get; set; } = null!;
        public string AddressInfo { get; set; } = null!;
        public bool? IsDefaultAddress { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
