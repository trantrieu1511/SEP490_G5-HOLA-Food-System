using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.Homepage
{
    public class ShopDto
    {
        public string UserId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string Avatar { get; set; }
        public decimal? Star { get; set; }
        public int NumberOrdered { get; set; }
    }

    /// <summary>
    /// Output of DisplayShop.
    /// </summary>
    public class DisplayShopOutputDto : BaseOutputDto
    {
        public List<ShopDto> ListShop { get; set; }
        public int total { get; set; }
    }
}
