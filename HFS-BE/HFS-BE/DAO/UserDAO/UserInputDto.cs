using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.DAO.UserDAO
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateUserDto : BaseInputDto
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class SearchShopInputDto : BaseInputDto
    {
        public string name { get; set; }
    }

}
