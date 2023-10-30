using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Mailjet.Client.Resources;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.Dao.FoodDao
{
    public class FoodDao : BaseDao
    {
        public FoodDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public FoodShopDaoOutputDto FoodByShop(FoodByShopDaoInputDto inputDto)
        {
            try
            {
                var data = this.context.Foods
                    .Include(x => x.FoodImages)
                    .Include(x => x.Category)
                    .Where(x => x.SellerId.Equals(inputDto.ShopId))
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
                    SellerId = inputDto.UserDto.UserId,
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

        public ListFoodOutputSellerDto GetAllFoodSeller(UserDto userDto)
        {
            try
            {
                List<FoodOutputSellerDto> foodsModel = context.Foods
                                        .Include(p => p.FoodImages)
                                        .Include(p => p.Category)
                                        .Include(p => p.Feedbacks)
                                        .Where(p => p.SellerId.Equals(userDto.UserId))
                                        .Select(p => new FoodOutputSellerDto
                                        {
                                            FoodId = p.FoodId,
                                            Name = p.Name,
                                            UnitPrice = p.UnitPrice,
                                            Description = p.Description,
                                            CategoryId = p.CategoryId,
                                            CategoryName = p.Category.Name,
                                            Status = PostMenuStatus.GetStatusString(p.Status),
                                            Images = p.FoodImages.ToList(),
                                            Feedbacks = p.Feedbacks.ToList(),
                                        })
                                        .ToList();
                var output = this.Output<ListFoodOutputSellerDto>(Constants.ResultCdSuccess);
                output.Foods = foodsModel;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<ListFoodOutputSellerDto>(Constants.ResultCdFail);
            }
        }

        public Food? GetFoodById(int foodId)
        {
            return context.Foods.FirstOrDefault(x => x.FoodId == foodId);
        }


        public BaseOutputDto EnableDisableFood(FoodEnableDisableInputDto input)
        {
            try
            {
                // Add food
                var food = context.Foods.FirstOrDefault(x => x.FoodId == input.FoodId);

                if (input.Type)
                {
                    // set status Display
                    food.Status = 1;
                    context.SaveChanges();
                    return Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                //set status Hide
                food.Status = 2;
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto UpdateFood(FoodUpdateInforInputDto input)
        {
            try
            {
                var foodModel = context.Foods.FirstOrDefault(
                        f => f.FoodId == input.FoodId
                    ); 
                if(foodModel ==  null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, $"FoodId: {input.FoodId} not exist!");

                foodModel.Name = input.Name;
                foodModel.UnitPrice = input.UnitPrice;
                foodModel.Description = input.Description;
                foodModel.CategoryId = input.CategoryId;

                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                //log error
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public Food GetFoodById(GetFoodByFoodIdDaoInputDto inputDto)
        {
            return context.Foods.FirstOrDefault(x => x.FoodId == inputDto.FoodId);
        }
    }
}
