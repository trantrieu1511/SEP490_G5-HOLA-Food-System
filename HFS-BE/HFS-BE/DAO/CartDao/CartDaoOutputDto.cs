using HFS_BE.Base;

namespace HFS_BE.DAO.CartDao
{
    public class GetCartItemDaoOutputDto : BaseOutputDto
    {
        public List<CartItemOutputDto> ListItem { get; set; }
    }


    public class CartItemOutputDto
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public int FoodId { get; set; }
        public int CartId { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Name { get; set; }
        public List<FoodImagesDto> foodImages { get; set; }
    }

    public class FoodImagesDto
    {
        public int ImageId { get; set; }
        public string Path { get; set; }
    }
}
