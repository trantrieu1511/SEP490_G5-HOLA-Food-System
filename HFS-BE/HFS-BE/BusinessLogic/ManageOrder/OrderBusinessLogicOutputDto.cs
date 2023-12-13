using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.OrderShipper;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class OrderDaoSellerOutputDto
    {
        public int? OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? OrderDate { get; set; }
        public string? RequiredDate { get; set; }
        public string? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public int? VoucherId { get; set; }
        public decimal DiscountAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string? ShippingType { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<DetailProgress>? OrderProgresses { get; set; }
        public List<OrderDetailFoodBLDto>? OrderDetails { get; set; }

    }


    public class OrderDetailFoodBLDto
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public FoodImageOutputSellerDto ImageBase64 { get; set; }
        public string CategoryName { get; set; }
    }

    public class DetailProgress
    {
        public ImageFoodOutputDto? ImageBase64 { get; set; }
        public string? Note { get; set; }
        public string? CreateDate { get; set; }
        public string? Status { get; set; }
    }

    public class OrderSellerDaoOutputDto : BaseOutputDto
    {
        public List<OrderDaoSellerOutputDto> Orders { get; set; }
    }
}
