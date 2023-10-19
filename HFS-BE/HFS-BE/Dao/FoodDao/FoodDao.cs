using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
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
                    .Where(x => x.ShopId == inputDto.ShopId)
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

        public BaseOutputDto AddNewFood(FoodCreateInputDto inputDto)
        {
            try
            {

                // Add food
                Food food = new Food
                {
                    ShopId = inputDto.UserDto.UserId,
                    Name = inputDto.Name,
                    UnitPrice = inputDto.UnitPrice,
                    Description = inputDto.Description,
                    CategoryId = inputDto.CategoryId,
                    Status = 0
                };
                context.Add(food);
                context.SaveChanges();

                foreach (var img in inputDto.Images)
                {
                    context.Add(new FoodImage
                    {
                        FoodId = food.FoodId,
                        Path = img
                    });
                    context.SaveChanges();
                }
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
