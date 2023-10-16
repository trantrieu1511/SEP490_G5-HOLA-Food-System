﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.CartDao
{
    public class CartDao : BaseDao
    {
        public CartDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetCartItemDaoOutputDto GetCartItem(GetCartItemDaoInputDto inputDto)
        {
            try
            {
                var data = this.context.CartItems
                    .Include(x => x.Food)
                    .ThenInclude(x => x.User)
                    .Include(x => x.Food)
                    .ThenInclude(x => x.FoodImages)
                    .Select(x => mapper.Map<CartItem, CartItemOutputDto>(x));

                var output = this.Output<GetCartItemDaoOutputDto>(Constants.ResultCdSuccess);
                if (data.Any())
                {
                    output.ListItem = data.ToList();
                }

                return output;

            }
            catch (Exception)
            {
                return this.Output<GetCartItemDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto AddCartItem(AddCartItemInputDto inputDto)
        {
            try
            {
                var data = this.context.CartItems
                    .Where(x => x.CartId == inputDto.CartId && x.FoodId == inputDto.FoodId)
                    .FirstOrDefault();

                if (data != null)
                {
                    data.Amount += inputDto.Amount;
                    context.Update(data);
                    context.SaveChanges();
                }

                var cartitem = mapper.Map<AddCartItemInputDto, CartItem>(inputDto);
                context.Add(cartitem);
                context.SaveChanges();
                
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}