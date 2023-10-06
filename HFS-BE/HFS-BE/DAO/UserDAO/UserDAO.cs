using HFS_BE.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace HFS_BE.DAO.UserDAO
{
    public class UserDAO
    {
        // dung` auto mapper => convert dto => object.
        // create : convert CreateUserto => User

        public SearchShopOututDto SearchShop(SearchShopInputDto inputDto)
        {
            SEP490_HFSContext context= new SEP490_HFSContext();
            var output = context.Foods.Where(x => x.Name.Equals(inputDto.name));
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
