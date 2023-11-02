using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.Cart
{
    public class CartDetailBusinessLogicOutputDto : BaseOutputDto
    {
        public List<ListShopItemDto> ListShop { get; set; } = new List<ListShopItemDto>();
    }

    public class ListShopItemDto
    {
        public string ShopId { get; set; }
        public string ShopName { get; set; }
        public List<CartItemDto> ListItem { get; set; }
    }

    public class CartItemDto
    {
        public int? FoodId { get; set; }
        public int? Amount { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Name { get; set; }
        public string foodImages { get; set; }
    }
}
