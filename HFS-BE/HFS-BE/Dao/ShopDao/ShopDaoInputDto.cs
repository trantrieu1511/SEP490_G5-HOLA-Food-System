using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.Dao.ShopDao
{
    public class CreateUserDto : BaseInputDto
    {

    }

    public class SearchShopInputDto : BaseInputDto
    {
        public string name { get; set; }
    }

}
