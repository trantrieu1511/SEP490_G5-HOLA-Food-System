using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.Cart
{
    public class CheckOutCartInputDto
    {
        public int? CustomerId { get; set; }
        public string? ShipAddress { get; set; }
        public int? VoucherId { get; set; }
        public string Note { get; set; }
        public List<ListShopItemDto> ListShop { get; set; } = new List<ListShopItemDto>();
    }

    public class ListShopItemInputDto
    {
        public int? ShopId { get; set; }
        public List<CartItemInputDto> CartItems { get; set; }
    }

    public class CartItemInputDto
    {
        public int FoodId { get; set; }
        public int Amount { get; set; }
    }
}
