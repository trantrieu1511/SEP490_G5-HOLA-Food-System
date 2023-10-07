using HFS_BE.Base;
using HFS_BE.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace HFS_BE.DAO.UserDAO
{
    public class UserDAO : BaseDao
    {
        public UserDAO(SEP490_HFSContext context) : base(context)
        {
        }

        // dung` auto mapper => convert dto => object.
        // create : convert CreateUserto => User

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public SearchShopOututDto SearchShop(SearchShopInputDto inputDto)
        {
            var output = this.context.Foods.Where(x => x.Name.Equals(inputDto.name));
            var outputDto = new SearchShopOututDto();

            foreach (var item in output)
            {
                var user = new UserDto()
                {
                    Id = item.FoodId,
                    Name = item.Name,
                };
                outputDto.ListUser.Add(user);
            }

            return outputDto;
        }
    }
}
