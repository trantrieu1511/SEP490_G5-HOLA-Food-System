using HFS_BE.Base;
using HFS_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using HFS_BE.Automapper;
using AutoMapper;
using HFS_BE.Ultis;

namespace HFS_BE.Dao.ShopDao
{
    public class ShopDao : BaseDao
    {
        public ShopDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }


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


        public DisplayShopOutputDto DisplayShop(BaseInputDto inputDto)
        {
            try
            {
                var output = this.context.Users.Where(x => x.RoleId == 2).ToList();

                DisplayShopOutputDto outputDto = this.Output<DisplayShopOutputDto>(Constants.ResultCdSuccess);
                //output = this.Paginate(output, inputDto.Pagination);
                outputDto.ListShop = mapper.Map<List<User>, List<ShopDto>>(output);
                return outputDto;
            }
            catch (Exception ex)
            {
                return this.Output<DisplayShopOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
