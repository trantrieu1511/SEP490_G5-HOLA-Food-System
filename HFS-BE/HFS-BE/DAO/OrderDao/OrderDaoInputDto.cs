using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Dao.OrderDao
{
    public class OrderByShipperDaoInputDto
    {
        [Required(ErrorMessage = "Shipper required!")]
        public string? ShipperId { get; set; }
        public bool Status { get; set; }
    }

    public class CartItemDaoInputDto
    {
        public int FoodId { get; set; }
        public int Amount { get; set; }
    }

    public class CheckOutOrderDaoInputDto
    {
        public string? CustomerId { get; set; }
        public string? ShopId { get; set; }
        public string? ShipAddress { get; set; }
        public int? VoucherId { get; set; }
        public string? Note { get; set; }
        public List<CartItemDaoInputDto> Items { get; set; }
    }
    public class OrderHistoryInputDto
    {
        public int? ShipperId { get; set; }
        public byte Status { get; set; }
    }

    public class OrderStatusInputDto
    {
        public int? ShipperId { get; set; }
        public int? OrderId { get; set; }
        public bool Status { get; set; }
    }

    public class OrderSellerByStatusInputDto
    {
        public string? UserId { get; set; }
        public int? Status { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateEnd { get; set; }
    }

    public class OrderInternalShipInputDto
    {
        public int OrderId { get; set; }
        public byte Status { get; set; }
        public string? UserId { get; set; }
        public string? ShipperId { get; set; }
    }
}
