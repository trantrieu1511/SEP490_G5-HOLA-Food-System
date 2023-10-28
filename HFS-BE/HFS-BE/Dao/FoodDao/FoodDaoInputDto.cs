using HFS_BE.Base;
using HFS_BE.Utils;

namespace HFS_BE.Dao.FoodDao
{
    public class FoodByShopDaoInputDto : BaseInputDto
    {
        public string? ShopId { get; set; }
    }

    public class GetFoodDetailDaoInputDto
    {
        public int? FoodId { get; set; }
    }

    public class FoodCreateInputDto
    {
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public List<string> Images { get; set; }
        public UserDto UserDto { get; set; }
    }

    public class FoodEnableDisableInputDto
    {
        public int FoodId { get; set; }
        public bool Type { get; set; }
    }

    public class FoodUpdateInforInputDto
    {
        public int FoodId { get; set; }
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }

    public class GetFoodByFoodIdDaoInputDto
    {
        public int? FoodId { get; set; }
    }
}
