using HFS_BE.Base;
using HFS_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using HFS_BE.Automapper;
using AutoMapper;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;
using HFS_BE.Dao.FoodDao;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;
using System.Text;

namespace HFS_BE.Dao.ShopDao
{
    public class ShopDao : BaseDao
    {
        public ShopDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }


        public DisplayShopDaoOutputDto DisplayShop(BaseInputDto inputDto)
        {
            try
            {
                var output = this.context.Sellers
                    .Include(x => x.Foods)
                    .ThenInclude(x => x.Feedbacks)
                    .Include(x => x.Foods)
                    .ThenInclude(x => x.FoodImages)
                    .Include(x => x.Orders)
                    .ThenInclude(x => x.OrderDetails)
                    .Where(x => x.Status == 1 && x.IsBanned == false && x.ConfirmedEmail.Value && x.IsPhoneVerified.Value).ToList();

                DisplayShopDaoOutputDto outputDto = this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdSuccess);
                //output = this.Paginate(output, inputDto.Pagination);
                var listshop = new List<ShopDto>();
                foreach (var item in output)
                {
                    int ordered = 0;
                    decimal star = 0;
                    int imgCount = 0;
                    var shop = mapper.Map<Seller, ShopDto>(item);

					foreach (var order in item.Orders)
                    {
						var data2 = context.OrderDetails.Include(s => s.Order).
         ThenInclude(s => s.OrderProgresses).Where(s => s.Order.OrderProgresses.Where(a => a.Status == 4).Any() && s.OrderId == order.OrderId);
						foreach (var orderdetail in data2)
                        {
                            ordered += orderdetail.Quantity.Value;
                        }
                    }

                    if (item.Foods.Any())
                    {
                        int totalStar = 0;
                        int totalfeedbacks = 0;
                        foreach (var food in item.Foods.Where(x => x.Status == 1))
                        {
                            foreach (var img in food.FoodImages)
                            {
                                if (imgCount == 3) break;
                                shop.FoodImages.Add(ImageFileConvert.ConvertFileToBase64(item.SellerId, img.Path, 1).ImageBase64);
                                imgCount++;
                            }

                            foreach (var feedback in food.Feedbacks)
                            {
                                totalStar += feedback.Star.Value;
                                totalfeedbacks++;
                            }
                        }

                        if (totalfeedbacks > 0)
                        {
                            star = (decimal)totalStar / (decimal)totalfeedbacks;
                        }
                    }

                    shop.Star = Math.Round(star, MidpointRounding.AwayFromZero);
                    shop.NumberOrdered = ordered;
                    if (this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(item.SellerId)) != null)
                    {
                        shop.Avatar = ImageFileConvert.ConvertFileToBase64(item.SellerId, this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(item.SellerId) && x.IsReplaced == false).Path, 3).ImageBase64;
                    }

                    listshop.Add(shop);
                }

                var data = listshop.Where(x => x.FoodImages.Count >= 3).ToList();
                outputDto.Total = data.Count();
                outputDto.ListShop = data.ToList();

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
                var output = this.context.Sellers
                    .Include(x => x.Foods)
                    .ThenInclude(x => x.Feedbacks)
                    .Include(x => x.Foods)
                    .ThenInclude(x => x.FoodImages)
                    .Include(x => x.Orders)
                    .ThenInclude(x => x.OrderDetails)
                    .Where(x => x.SellerId.Equals(inputDto.ShopId) && x.Status == 1 && !x.IsBanned.Value && x.ConfirmedEmail.Value && x.IsPhoneVerified.Value)
                    .FirstOrDefault();

                
                var outputDto = this.Output<GetShopDetailDaoOutputDto>(Constants.ResultCdSuccess);
                if (output != null)
                {
                    int ordered = 0;
                    decimal star = 0;
                    int imgCount = 0;
                    foreach (var order in output.Orders)
                    {
						var data2 = context.OrderDetails.Include(s => s.Order).
				  ThenInclude(s => s.OrderProgresses).Where(s => s.Order.OrderProgresses.Where(a => a.Status == 4).Any() && s.OrderId == order.OrderId);
						foreach (var orderdetail in data2)
                        {
                            ordered += orderdetail.Quantity.Value;
                        }
                    }

                    if (output.Foods.Any())
                    {
                        int totalStar = 0;
                        int totalfeedbacks = 0;
                        foreach (var food in output.Foods)
                        {
                            foreach (var feedback in food.Feedbacks)
                            {
                                totalStar += feedback.Star.Value;
                                totalfeedbacks++;
                            }
                        }

                        if (totalfeedbacks > 0)
                        {
                            star = (decimal)totalStar / (decimal)totalfeedbacks;
                        }
                    }

                    outputDto = mapper.Map<Seller, GetShopDetailDaoOutputDto>(output);
                    if (this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(output.SellerId)) != null)
                    {
                        outputDto.Avatar = ImageFileConvert.ConvertFileToBase64(output.SellerId, this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(output.SellerId) && x.IsReplaced == false).Path, 3).ImageBase64;
                    }

                    outputDto.AverageStar = Math.Round(star, MidpointRounding.AwayFromZero);
                    outputDto.NumberOrdered = ordered;
                    outputDto.TotalFood = output.Foods.Where(x => x.SellerId.Equals(inputDto.ShopId) && x.Status == 1).Count();
                }

                return outputDto;
            }
            catch (Exception ex)
            {
                return this.Output<GetShopDetailDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public DisplayShopDaoOutputDto DisplayShopByName(SearchShopInputDto inputDto)
        {
            try
            {
                var key = RemoveAccents(inputDto.Key);
                var output = this.context.Sellers
                    .Include(x => x.Foods)
                    .ThenInclude(x => x.Feedbacks)
                    .Include(x => x.Orders)
                    .ThenInclude(x => x.OrderDetails)
                    .Where(x => x.Status == 1 && x.IsBanned == false && x.ConfirmedEmail.Value && x.IsPhoneVerified.Value)
                    .ToList();

                DisplayShopDaoOutputDto outputDto = this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdSuccess);
                //output = this.Paginate(output, inputDto.Pagination);
                var listshop = new List<ShopDto>();
                foreach (var item in output)
                {
                    if (!string.IsNullOrEmpty(key) && (!RemoveAccents(item.ShopName).Contains(key) && !item.SellerId.Contains(key)))
                    {
                        continue;
                    }
                    int ordered = 0;
                    decimal star = 0;
                    foreach (var order in item.Orders)
                    {
						var data2 = context.OrderDetails.Include(s => s.Order).
			  ThenInclude(s => s.OrderProgresses).Where(s => s.Order.OrderProgresses.Where(a => a.Status == 4).Any() && s.OrderId == order.OrderId);
						foreach (var orderdetail in data2)
                        {
                            ordered += orderdetail.Quantity.Value;
                        }
                    }

                    if (item.Foods.Any())
                    {
                        int totalStar = 0;
                        int totalfeedbacks = 0;
                        foreach (var food in item.Foods)
                        {
                            foreach (var feedback in food.Feedbacks)
                            {
                                totalStar += feedback.Star.Value;
                                totalfeedbacks++;
                            }
                        }
                        if (totalfeedbacks > 0)
                        {
                            star = (decimal)totalStar / (decimal)totalfeedbacks;
                        }
                    }

                    var shop = mapper.Map<Seller, ShopDto>(item);
                    shop.Star = Math.Round(star, MidpointRounding.AwayFromZero);
                    shop.NumberOrdered = ordered;
                    if (this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(item.SellerId)) != null)
                    {
                        shop.Avatar = ImageFileConvert.ConvertFileToBase64(item.SellerId, this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(item.SellerId) && !x.IsReplaced).Path, 3).ImageBase64;
                    }

                    listshop.Add(shop);
                }

                outputDto.Total = listshop.Count();
                outputDto.ListShop = listshop.ToList();

                return outputDto;
            }
            catch (Exception ex)
            {
                return this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        private string RemoveAccents(string input)
        {
            try
            {
                string normalized = input.Normalize(NormalizationForm.FormKD);
                Regex regex = new Regex("[\\p{Mn}]", RegexOptions.Compiled);
                return regex.Replace(normalized, string.Empty).ToLower();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
