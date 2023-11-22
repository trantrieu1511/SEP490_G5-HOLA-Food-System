using AutoMapper;
using CloudinaryDotNet.Actions;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using HFS_BE.Utils.IOFile;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

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
                    .Include(x => x.OrderDetails)
                    .Include(x => x.Feedbacks)
                    .Where(x => x.SellerId.Equals(inputDto.ShopId) && x.Status == 1)
                    .ToList();

                var listfood = new List<FoodOutputDto>();
                foreach (var item in data)
                {
                    int ordered = 0;
                    decimal star = 0;
                    foreach (var orderdetail in item.OrderDetails)
                    {
                        ordered += orderdetail.Quantity.Value;
                    }

                    if (item.Feedbacks.Any())
                    {
                        int totalStar = 0;
                        foreach (var feedback in item.Feedbacks)
                        {
                            totalStar += feedback.Star.Value;
                        }
                        star = (decimal)totalStar / (decimal)item.Feedbacks.Count();
                    }

                    var food = mapper.Map<Food, FoodOutputDto>(item);
                    food.AverageStar = Math.Round(star, MidpointRounding.AwayFromZero);
                    food.NumberOrdered = ordered;


                    foreach (var img in food.foodImages)
                    {
                        var imageInfor = ImageFileConvert.ConvertFileToBase64(inputDto.ShopId, img.Name, 1);
                        img.Size = imageInfor.Size;
                        img.ImageBase64 = imageInfor.ImageBase64;
                    }

                    listfood.Add(food);
                }


                var output = this.Output<FoodShopDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListFood = listfood;
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
                    .Where(x => x.FoodId == inputDto.FoodId && x.Status == 1)
                    .FirstOrDefault();

                var shopid = data.SellerId;

                var output = this.Output<FoodOutputDto>(Constants.ResultCdSuccess);
                output = mapper.Map<Food, FoodOutputDto>(data);
                foreach (var img in output.foodImages)
                {
                    var imageInfor = ImageFileConvert.ConvertFileToBase64(shopid, img.Name, 1);
                    img.Size = imageInfor.Size;
                    img.ImageBase64 = imageInfor.ImageBase64;
                }
                return output;
            }
            catch (Exception)
            {
                return this.Output<FoodOutputDto>(Constants.ResultCdFail);
            }
        }

        public AddFoodOutput AddNewFood(FoodCreateInputDto inputDto)
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
                var output = Output<AddFoodOutput>(Constants.ResultCdSuccess);
                output.FoodId = food.FoodId;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<AddFoodOutput>(Constants.ResultCdFail);
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
                                            Status = PostMenuStatusEnum.GetStatusString(p.Status),
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

        public ListFoodOutputSellerDto GetAllFoodMenuModerator()
        {
            try
            {
                List<FoodOutputSellerDto> foodsModel = context.Foods
                                        .Include(p => p.FoodImages)
                                        .Include(p => p.Category)
                                        .Include(p => p.Feedbacks)
                                        .Select(p => new FoodOutputSellerDto
                                        {
                                            FoodId = p.FoodId,
                                            SellerId = p.SellerId,
                                            Name = p.Name,
                                            UnitPrice = p.UnitPrice,
                                            Description = p.Description,
                                            CategoryId = p.CategoryId,
                                            CategoryName = p.Category.Name,
                                            Status = PostMenuStatusEnum.GetStatusString(p.Status),
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
            return context.Foods
                .Include(x => x.Seller)
                .FirstOrDefault(x => x.FoodId == foodId);
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

        public BaseOutputDto BanUnbanFood(FoodBanUnbanInputDto input, string userId)
        {
            try
            {
                // Check user role
                if (userId.Substring(0, 2) != "MM")
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a menu moderator before using this API");
                }
                // Check ban limit (25 per day), neu lon hon 0 thi moi thuc hien viec ban, neu khong thi se khong thuc hien viec ban nua. 
                // Phong truong hop co nguoi lam nguoi khong lam (BanLimit duoc reset vao 23h59 moi ngay)
                if (context.MenuModerators.SingleOrDefault(mm => mm.ModId.Equals(userId)).BanLimit > 0)
                {
                    // Get food
                    var food = context.Foods.SingleOrDefault(x => x.FoodId == input.FoodId);

                    // Check food is found or not
                    if (food == null)
                    {
                        return Output<BaseOutputDto>(Constants.ResultCdFail, "Food not found");
                    }

                    if (input.isBanned)
                    {
                        // set status banned
                        food.Status = 3;
                    }
                    else
                    {
                        //set status display
                        food.Status = 1;
                    }

                    // Reduce ban/unban limit
                    context.MenuModerators.SingleOrDefault(mm => mm.ModId.Equals(userId)).BanLimit -= 1;

                    context.SaveChanges();
                    return Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                else
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Your limit of banning 25 food per day have reached.");
                }
            }
            catch (Exception)
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
                if (foodModel == null)
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

        public FoodShopDaoOutputDto GetFoodByCategory(GetFoodByCategoryDaoInputDto inputDto)
        {
            try
            {
                var data = this.context.Foods
                    .Include(x => x.FoodImages)
                    .Include(x => x.Category)
                    .Where(x => x.CategoryId.Equals(inputDto.CategoryId) && x.Status == 1)
                    .ToList();

                var output = this.Output<FoodShopDaoOutputDto>(Constants.ResultCdSuccess);
                var listfood = new List<FoodOutputDto>();
                foreach (var item in data)
                {
                    int ordered = 0;
                    decimal star = 0;
                    foreach (var orderdetail in item.OrderDetails)
                    {
                        ordered += orderdetail.Quantity.Value;
                    }

                    if (item.Feedbacks.Any())
                    {
                        int totalStar = 0;
                        foreach (var feedback in item.Feedbacks)
                        {
                            totalStar += feedback.Star.Value;
                        }
                        star = (decimal)totalStar / (decimal)item.Feedbacks.Count();
                    }

                    var food = mapper.Map<Food, FoodOutputDto>(item);
                    food.AverageStar = Math.Round(star, MidpointRounding.AwayFromZero);
                    food.NumberOrdered = ordered;


                    foreach (var img in food.foodImages)
                    {
                        var imageInfor = ImageFileConvert.ConvertFileToBase64(item.SellerId, img.Name, 1);
                        img.Size = imageInfor.Size;
                        img.ImageBase64 = imageInfor.ImageBase64;
                    }

                    listfood.Add(food);
                }

                output.ListFood = listfood;
                return output;
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public FoodShopDaoOutputDto GetHotFood()
        {
            try
            {
                var data = this.context.Foods
                    .Include(x => x.FoodImages)
                    .Include(x => x.Category)
                    .Include(x => x.OrderDetails)
                    .Include(x => x.Feedbacks)
                    .Where(x => x.Status == 1)
                    .ToList();

                var listfood = new List<FoodOutputDto>();
                foreach (var item in data)
                {
                    int ordered = 0;
                    decimal star = 0;
                    foreach (var orderdetail in item.OrderDetails)
                    {
                        ordered += orderdetail.Quantity.Value;
                    }

                    if (item.Feedbacks.Any())
                    {
                        int totalStar = 0;
                        foreach (var feedback in item.Feedbacks)
                        {
                            totalStar += feedback.Star.Value;
                        }
                        star = (decimal)totalStar / (decimal)item.Feedbacks.Count();
                    }

                    var food = mapper.Map<Food, FoodOutputDto>(item);
                    food.AverageStar = Math.Round(star, MidpointRounding.AwayFromZero);
                    food.NumberOrdered = ordered;


                    foreach (var img in food.foodImages)
                    {
                        var imageInfor = ImageFileConvert.ConvertFileToBase64(item.SellerId, img.Name, 1);
                        img.Size = imageInfor.Size;
                        img.ImageBase64 = imageInfor.ImageBase64;
                    }

                    listfood.Add(food);
                }

                listfood = listfood.OrderByDescending(x => x.NumberOrdered).ThenByDescending(x => x.AverageStar).Take(10).ToList();

                var output = this.Output<FoodShopDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListFood = listfood;
                return output;
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public FoodShopDaoOutputDto FoodByName(FoodByNameDaoInputDto inputDto)
        {
            try
            {
                var key = RemoveAccents(inputDto.SearchKey);
                var data = this.context.Foods
                    .Include(x => x.FoodImages)
                    .Include(x => x.Category)
                    .Include(x => x.OrderDetails)
                    .Include(x => x.Feedbacks)
                    .Where(x => x.Status == 1)
                    .ToList();

                var listfood = new List<FoodOutputDto>();
                foreach (var item in data)
                {
                    if (!RemoveAccents(item.Name).Contains(key))
                    {
                        continue;
                    }
                    int ordered = 0;
                    decimal star = 0;
                    foreach (var orderdetail in item.OrderDetails)
                    {
                        ordered += orderdetail.Quantity.Value;
                    }

                    if (item.Feedbacks.Any())
                    {
                        int totalStar = 0;
                        foreach (var feedback in item.Feedbacks)
                        {
                            totalStar += feedback.Star.Value;
                        }
                        star = (decimal)totalStar / (decimal)item.Feedbacks.Count();
                    }

                    var food = mapper.Map<Food, FoodOutputDto>(item);
                    food.AverageStar = Math.Round(star, MidpointRounding.AwayFromZero);
                    food.NumberOrdered = ordered;


                    foreach (var img in food.foodImages)
                    {
                        var imageInfor = ImageFileConvert.ConvertFileToBase64(item.SellerId, img.Name, 1);
                        img.Size = imageInfor.Size;
                        img.ImageBase64 = imageInfor.ImageBase64;
                    }

                    listfood.Add(food);
                }


                var output = this.Output<FoodShopDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListFood = listfood;
                return output;
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        private string RemoveAccents(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormKD);
            Regex regex = new Regex("[\\p{Mn}]", RegexOptions.Compiled);
            return regex.Replace(normalized, string.Empty).ToLower();
        }
    }
}