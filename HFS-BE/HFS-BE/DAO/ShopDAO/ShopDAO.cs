﻿using HFS_BE.Base;
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


        public DisplayShopDaoOutputDto DisplayShop()
        {
            try
            {
                var output = this.context.Sellers
                    .Include(x => x.Foods)
                    .ThenInclude(x => x.Feedbacks)
                    .Include(x => x.Orders)
                    .ThenInclude(x => x.OrderDetails)
                    .Where(x => x.IsVerified == true).ToList();

                DisplayShopDaoOutputDto outputDto = this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdSuccess);
                //output = this.Paginate(output, inputDto.Pagination);
                var listshop = new List<ShopDto>();
                foreach (var item in output)
                {
                    int ordered = 0;
                    decimal star = 0;
                    foreach (var order in item.Orders)
                    {
                        foreach (var orderdetail in order.OrderDetails)
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
                        shop.Avatar = ImageFileConvert.ConvertFileToBase64(item.SellerId, this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(item.SellerId)).Path, 2).ImageBase64;
                    }
                    
                    listshop.Add(shop);
                }

                outputDto.ListShop = listshop;


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
                    .Where(x => x.SellerId.Equals(inputDto.ShopId) && x.IsVerified == true)
                    .FirstOrDefault();

                var outputDto = this.Output<GetShopDetailDaoOutputDto>(Constants.ResultCdSuccess);
                if (output != null)
                {
                    outputDto = mapper.Map<Seller, GetShopDetailDaoOutputDto>(output);
                    if (this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(output.SellerId)) != null)
                    {
                        outputDto.Avatar = ImageFileConvert.ConvertFileToBase64(output.SellerId, this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(output.SellerId)).Path, 2).ImageBase64;
                    } 
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
                    .Where(x => x.IsVerified == true).ToList();

                DisplayShopDaoOutputDto outputDto = this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdSuccess);
                //output = this.Paginate(output, inputDto.Pagination);
                var listshop = new List<ShopDto>();
                foreach (var item in output)
                {
                    if (!RemoveAccents(item.ShopName).Contains(key))
                    {
                        continue;
                    }
                    int ordered = 0;
                    decimal star = 0;
                    foreach (var order in item.Orders)
                    {
                        foreach (var orderdetail in order.OrderDetails)
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
                    shop.Avatar = ImageFileConvert.ConvertFileToBase64(item.SellerId, this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(item.SellerId)).Path, 2).ImageBase64;
                    listshop.Add(shop);
                }

                outputDto.ListShop = listshop;


                return outputDto;
            }
            catch (Exception ex)
            {
                return this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdFail);
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
