using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.Dao.ShopDao
{
    public class SearchShopInputDto : BaseInputDto
    {
        public string Key { get; set; }
    }

    public class GetShopDetailDaoInputDto
    {
        public string ShopId { get; set; }
    }
}
