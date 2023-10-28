using HFS_BE.Base;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.BusinessLogic.Cart
{
    public class CheckOutCartInputDto
    {
        public int? CustomerId { get; set; }
        [Required(ErrorMessage = "ShipAddress Required")]
        public string? ShipAddress { get; set; }
        public int? VoucherId { get; set; }
        public string? Note { get; set; }
        [Required(ErrorMessage = "Phone Required")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "PaymentMethod Required")]
        public string? PaymentMethod { get; set; }
        public List<ListShopItemInputDto> ListShop { get; set; }
    }

    public class ListShopItemInputDto
    {
        [Required(ErrorMessage = "ShopId Required!")]
        public int? ShopId { get; set; }
        public List<CartItemInputDto>? CartItems { get; set; }
    }

    public class CartItemInputDto
    {
        [Required(ErrorMessage = "FoodId Required")]
        public int? FoodId { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must >0")]
        public int? Amount { get; set; }
    }
}
