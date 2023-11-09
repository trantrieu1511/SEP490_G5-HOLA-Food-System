using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.CartDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Cart
{
    public class CheckOutBusinessLogic : BaseBusinessLogic
    {
        public CheckOutBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CheckOutCart(CheckOutCartInputDto inputDto)
        {
            try
            {
                var orderDao = this.CreateDao<OrderDao>();
                var orderProgessDao = this.CreateDao<OrderProgressDao>();
                var cartDao = this.CreateDao<CartDao>();
                var userDao = this.CreateDao<UserDao>();
                var foodDao = this.CreateDao<FoodDao>();
                var notifyDao = CreateDao<NotificationDao>();
                var user = userDao.GetUserInfo(new GetOrderInfoInputDto()
                {
                    UserId = inputDto.CustomerId,
                });

                if (!inputDto.PaymentMethod.Equals("wallet") && !inputDto.PaymentMethod.Equals("cod"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Your payment method invalid!");
                }

                if(inputDto.ListShop.Count == 0)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Your ListShop is empty!");
                }

                decimal totalPrice = 0;
                foreach (var item in inputDto.ListShop)
                {
                    if (item.CartItems.Count == 0)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Your CartItems of ShopId " + item.ShopId + " is empty!");
                    }
                    foreach (var food in item.CartItems)
                    {
                        var foodInfo = foodDao.GetFoodById(new GetFoodByFoodIdDaoInputDto
                        {
                            FoodId = food.FoodId,
                        });
                        if (foodInfo == null)
                        {
                            return this.Output<BaseOutputDto>(Constants.ResultCdFail, "FoodId not exsit!");
                        }

                        totalPrice += food.Amount.Value * foodInfo.UnitPrice.Value;
                    }
                }

                if (totalPrice > user.Balance && inputDto.PaymentMethod.Equals("wallet"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess, "Balance not enough!");
                }
                
                foreach (ListShopItemInputDto item in inputDto.ListShop)
                {
                    // create order + order detail
                    CheckOutOrderDaoInputDto orderdaoInput = mapper.Map<ListShopItemInputDto, CheckOutOrderDaoInputDto>(item);
                    orderdaoInput.CustomerId = inputDto.CustomerId;
                    orderdaoInput.ShipAddress = inputDto.ShipAddress;
                    orderdaoInput.VoucherId = inputDto.VoucherId;
                    orderdaoInput.Note = inputDto.Note;
                    orderdaoInput.PaymentMethod = inputDto.PaymentMethod.Equals("wallet") ? 1 : 0;

                    var daoOutput = orderDao.CheckOutOrder(orderdaoInput);
                    if (!daoOutput.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }

                    // create order progress
                    var progressInput = new OrderCreateDaoInputDto()
                    {
                        Note = "Order success! Wait seller.",
                        CreateDate = DateTime.Now,
                        OrderId = daoOutput.OrderId,
                        UserId = inputDto.CustomerId,
                        Status = 0,
                    };

                    var progressOutput = orderProgessDao.CreateOrderProgressCustomer(progressInput);
                    if (!progressOutput.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }

                    // create noti for user and seller.
                    // add new nofition
                    NotificationAddNewInputDto inputNoti = new NotificationAddNewInputDto
                    {
                        CreateDate = DateTime.Now,
                        SendBy = "System",
                        Receiver = item.ShopId,
                        TypeId = 1
                    };
                    // gen title and content notification
                    GenerateNotification.GetSingleton().GenNotificationNewOrderSeller(inputNoti, (int)daoOutput.OrderId);
                    var noti = notifyDao.AddNewNotification(inputNoti);
                    if (!noti.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }

                    // delete item from cart.
                    foreach (var cartitem in item.CartItems)
                    {
                        DeleteCartItemInputDto iteminput = new DeleteCartItemInputDto()
                        {
                            FoodId = cartitem.FoodId.Value,
                            CartId = inputDto.CustomerId,
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
