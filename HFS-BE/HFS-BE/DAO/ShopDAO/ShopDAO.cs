using HFS_BE.Base;
using HFS_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using HFS_BE.Automapper;
using AutoMapper;
using HFS_BE.Utils;

namespace HFS_BE.Dao.ShopDao
{
    public class ShopDao : BaseDao
    {
        public ShopDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }


        public DisplayShopDaoOutputDto DisplayShop()
        {
            try
            {
                var output = this.context.Users.Where(x => x.RoleId == 2).ToList();

                DisplayShopDaoOutputDto outputDto = this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdSuccess);
                //output = this.Paginate(output, inputDto.Pagination);
                outputDto.ListShop = mapper.Map<List<User>, List<ShopDto>>(output);
                return outputDto;
            }
            catch (Exception ex)
            {
                return this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public GetShopDetailDaoOutputDto GetShopDetail(GetShopDetailDaoInputDto inputDto)
        {
            try
            {
                var output = this.context.Users
                    .Where(x => x.UserId == inputDto.ShopId)
                    .FirstOrDefault();

                var outputDto = this.Output<GetShopDetailDaoOutputDto>(Constants.ResultCdSuccess);
                if (output != null)
                {
                    outputDto = mapper.Map<User, GetShopDetailDaoOutputDto>(output);
                }
                
                return outputDto;
            }
            catch (Exception ex)
            {
                return this.Output<GetShopDetailDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
