using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Category
    {
        public Category()
        {
            Foods = new HashSet<Food>();
        }

        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}
