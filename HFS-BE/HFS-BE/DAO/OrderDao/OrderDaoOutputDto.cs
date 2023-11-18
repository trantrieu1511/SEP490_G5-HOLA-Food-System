using HFS_BE.Base;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;

namespace HFS_BE.Dao.OrderDao
{
    public class OrderDaoOutputDto
    {
        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? SellerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; } 
        public DateTime? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipperId { get; set; }
        public int? VoucherId { get; set; }
        public string? Status { get; set; }

        public virtual List<OrderDetailDto> OrderDetails { get; set; }
        public virtual List<OrderProgressDto> OrderProgresses { get; set; }

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
        public string SellerId { get; set; }
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
        public string? CreatedBy { get; set; }
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


    public class OrderDaoSellerOutputDto
    {
        public int? OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? OrderDate { get; set; }
        public string? RequiredDate { get; set; }
        public string? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public int? VoucherId { get; set; }
        public decimal DiscountAmount { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<DetailProgress>? OrderProgresses { get; set; }
        public List<OrderDetailFoodDto>? OrderDetails { get; set; }

    }
    

    public class OrderDetailFoodDto
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public string? FoodName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string Image { get; set; }
        public string? CategoryName { get; set; }
        public string? SellId { get; set; }
    }

    public class DetailProgress
    {
        public string? Image { get; set; }
        public string? Note { get; set; }
        public string? CreateDate { get; set; }
    }

    public class OrderSellerDaoOutputDto : BaseOutputDto
    {
        public List<OrderDaoSellerOutputDto> Orders { get; set; }
    }

    public class OrderCustomerDaoOutputDto : BaseOutputDto
    {
        public int? OrderId { get; set; }
        public string? SellerId { get; set; }
        public string? ShopName { get; set; }
        public string? OrderDate { get; set; }
        public string? RequiredDate { get; set; }
        public string? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public int? VoucherId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Status { get; set; }
        public List<DetailProgressCustomerDto>? OrderProgresses { get; set; }
        public List<OrderDetaiCustomerDto>? OrderDetails { get; set; }
    }

    public class OrderDetaiCustomerDto
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public string? FoodName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string Image { get; set; }
        public string? CategoryName { get; set; }
        public string? SellerId { get; set; }
        public bool? IsRated { get; set; }
    }

    public class DetailProgressCustomerDto
    {
        public string? Image { get; set; }
        public string? Note { get; set; }
        public string? CreateDate { get; set; }
    }

    public class GetCustomerOrdersDaoOutputDto : BaseOutputDto
    {
        public List<OrderCustomerDaoOutputDto> ListOrders { get; set;}
    }

    public class OrderExternalShipperOutputDto
    {
        public int? OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? OrderDate { get; set; }
        public string? ShipAddress { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<OrderDetailFoodDto>? OrderDetails { get; set; }

    }

    public class OrderExternalLstOutputDto : BaseOutputDto
    {
        public List<OrderExternalShipperOutputDto> Orders { get; set; }
    }

    public class DashboardSellerDaoOutput : BaseOutputDto
    {
        public ICollection<Order> Orders { get; set; }
    }
}
