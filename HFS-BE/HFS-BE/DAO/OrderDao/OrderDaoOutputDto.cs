using HFS_BE.Base;
using HFS_BE.DAO.OrderProgressDao;
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
        public string? Status { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }

    }

    public class OrderByShipperDaoOutputDto : BaseOutputDto
    {
        public List<OrderDaoOutputDto> Orders { get; set; }
    }
    public class OrderHistoryDaoOutputDto : BaseOutputDto
    {
        public List<OrderDaoOutputDto> Orders { get; set; }
    }
    public class OrderDetailDto
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int ShopId { get; set; }
        public string FoodName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string Image { get; set; }
    }
    public class OrderProgressDto
    {
        public int OrderProgressId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Image { get; set; }
        public int? OrderId { get; set; }
        public byte? Status { get; set; }
        public int? UserId { get; set; }
    }

    public class CheckOutOrderDaoOutputDto : BaseOutputDto
    {
        public int? OrderId { get; set; }
    }
    public class OrderOnHistoryDaoOutputDto : BaseOutputDto
    {
        public List<OrderDaoOutputDto> OrderList { get; set; }
    }

    public class OrderHistoryDetailDtoOutput : BaseOutputDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public int? ShipperId { get; set; }
        public int? VoucherId { get; set; }
        public byte? Status { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }
        public List<OrderProgressDaoOutputDto> OrderProgresses { get; set; }
    }
}
