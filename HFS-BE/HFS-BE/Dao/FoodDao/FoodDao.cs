using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.Dao.FoodDao
{
    public class FoodDao : BaseDao
    {
        public FoodDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public FoodShopDaoOutputDto FoodByShop(FoodByShopDaoInputDto inputDto)
        {
            try
            {
                var data = this.context.Foods
                    .Include(x => x.FoodImages)
                    .Include(x => x.Category)
                    .Where(x => x.User.UserId == inputDto.ShopId)
                    .Select(x => mapper.Map<Food, FoodOutputDto>(x))
                    .ToList();

                var output = this.Output<FoodShopDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListFood = data;
                return output;
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public FoodOutputDto GetFoodDetail(GetFoodDetailDaoInputDto inputDto)
        {
            try
            {
                var data = this.context.Foods
                    .Include(x => x.FoodImages)
                    .Include(x => x.Category)
                    .Where(x => x.FoodId == inputDto.FoodId)
                    .FirstOrDefault();

                var output = this.Output<FoodOutputDto>(Constants.ResultCdSuccess);
                return output = mapper.Map<Food, FoodOutputDto>(data);
            }
            catch (Exception)
            {
                return this.Output<FoodOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
