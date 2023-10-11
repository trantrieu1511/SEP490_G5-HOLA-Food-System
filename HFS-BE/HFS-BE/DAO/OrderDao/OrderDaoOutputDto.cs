﻿using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.Dao.OrderDao
{
    public class OrderDaoOutputDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public int? ShipperId { get; set; }
        public int? VoucherId { get; set; }
        public bool? Status { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }
    }

    public class OrderByShipperDaoOutputDto : BaseOutputDto
    {
        public List<OrderDaoOutputDto> OrderList { get; set; }
    }
    public class OrderDetailDto
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }
    }
}