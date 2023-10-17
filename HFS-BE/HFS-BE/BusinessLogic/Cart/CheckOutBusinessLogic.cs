using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.CartDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Cart
{
    public class CheckOutBusinessLogic : BaseBusinessLogic
    {
        public CheckOutBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CheckOutCart(CheckOutCartInputDto inputDto)
        {
            try
            {
                var orderDao = this.CreateDao<OrderDao>();
                var orderProgessDao = this.CreateDao<OrderProgressDao>();
                var cartDao = this.CreateDao<CartDao>();

                
                foreach (ListShopItemDto item in inputDto.ListShop)
                {
                    // create order + order detail
                    CheckOutOrderDaoInputDto orderdaoInput = mapper.Map<ListShopItemDto, CheckOutOrderDaoInputDto>(item);
                    orderdaoInput.CustomerId = inputDto.CustomerId;
                    orderdaoInput.ShipAddress = inputDto.ShipAddress;
                    orderdaoInput.VoucherId = inputDto.VoucherId;
                    orderdaoInput.Note = inputDto.Note;

                    var daoOutput = orderDao.CheckOutOrder(orderdaoInput);
                    if (!daoOutput.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }

                    // create order progress
                    var progressInput = new OrderProgressDaoInputDto()
                    {
                        Note = "Order success! Wait seller.",
                        CreateDate = DateTime.Now,
                        OrderId = daoOutput.OrderId,
                        UserId = inputDto.CustomerId,
                    };

                    var progressOutput = orderProgessDao.CreateOrderProgress(progressInput);
                    if (!progressOutput.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }

                    // create noti for user and seller.

                    // delete item from cart.
                    foreach (var cartitem in item.ListItem)
                    {
                        DeleteCartItemInputDto iteminput = new DeleteCartItemInputDto()
                        {
                            FoodId = cartitem.FoodId,
                            CartId = inputDto.CustomerId.Value,
                        };

                        var deletecartitem = cartDao.DeleteCartItem(iteminput);
                        if (!deletecartitem.Success)
                        {
                            return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                        }
                    }
                }

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
