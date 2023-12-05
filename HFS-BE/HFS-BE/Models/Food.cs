using System;
using System.Collections.Generic;

namespace HFS_BE.Models
{
    public partial class Food
    {
        public Food()
        {
            CartItems = new HashSet<CartItem>();
            Feedbacks = new HashSet<Feedback>();
            FoodImages = new HashSet<FoodImage>();
            MenuReports = new HashSet<MenuReport>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int FoodId { get; set; }
        public string SellerId { get; set; } = null!;
        public string? Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public byte? Status { get; set; }
        public int? ReportedTimes { get; set; }
        public string? BanBy { get; set; }
        public DateTime? BanDate { get; set; }
        public string? BanNote { get; set; }

        public virtual MenuModerator? BanByNavigation { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Seller Seller { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<FoodImage> FoodImages { get; set; }
        public virtual ICollection<MenuReport> MenuReports { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
