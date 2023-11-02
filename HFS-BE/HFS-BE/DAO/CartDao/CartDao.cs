using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.CartDao
{
    public class CartDao : BaseDao
    {
        public CartDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetCartItemDaoOutputDto GetCartItem(GetCartItemDaoInputDto inputDto)
        {
            try
            {
                var data = this.context.CartItems
                    .Include(x => x.Food)
                    .ThenInclude(x => x.Seller)
                    .Include(x => x.Food)
                    .ThenInclude(x => x.FoodImages)
                    .Where(x => x.CartId == inputDto.CartId)
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
                var food = this.context.Foods.FirstOrDefault(x => x.FoodId == inputDto.FoodId);
                if (food == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Food is not exsit in HFS.");
                }
                var data = this.context.CartItems
                    .Where(x => x.CartId == inputDto.CartId && x.FoodId == inputDto.FoodId)
                    .FirstOrDefault();

                if (data != null)
                {
                    data.Amount += inputDto.Amount.Value;
                    context.Update(data);
                    context.SaveChanges();
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
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

        public BaseOutputDto DeleteCartItem(DeleteCartItemInputDto inputDto)
        {
            try
            {
                var data = this.context.CartItems
                    .Where(x => x.CartId == inputDto.CartId && x.FoodId == inputDto.FoodId)
                    .FirstOrDefault();

                if (data != null && inputDto.Amount != null)
                {
                    data.Amount -= inputDto.Amount.Value;
                    context.Update(data);
                    context.SaveChanges();
                }
                else if (data != null)
                {
                    context.Remove(data);
                    context.SaveChanges();
                }
               
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto UpdateAmoutCartItem(UpdateAmoutCartItemDaoInputDto inputDto)
        {
            try
            {
                var cartitem = this.context.CartItems.FirstOrDefault(x => x.CartId.Equals(inputDto.CartId) && x.FoodId == inputDto.FoodId);

                if (cartitem != null)
                {
                    cartitem.Amount = inputDto.Amount.Value;
                }

                context.Update(cartitem);
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
