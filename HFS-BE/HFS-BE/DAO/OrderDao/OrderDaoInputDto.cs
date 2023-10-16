using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Dao.OrderDao
{
    public class OrderByShipperDaoInputDto
    {
        [Required(ErrorMessage = "Shipper required!")]
        public int? ShipperId { get; set; }
    }

    public class CartItemDaoInputDto
    {
        public int FoodId { get; set; }
        public int Amount { get; set; }
    }

    public class CheckOutOrderDaoInputDto
    {
        public List<CartItemDaoInputDto> Items { get; set; }
    }
    public class OrderHistoryInputDto
    {
        public int? ShipperId { get; set; }
        public bool Status { get; set; }
    }
}
