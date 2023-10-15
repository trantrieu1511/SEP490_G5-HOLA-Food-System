using HFS_BE.Base;

namespace HFS_BE.Dao.FoodDao
{
    public class FoodByShopDaoInputDto : BaseInputDto
    {
        public int? ShopId { get; set; }
    }

    public class GetFoodDetailDaoInputDto
    {
        public int? FoodId { get; set; }
    }
}
