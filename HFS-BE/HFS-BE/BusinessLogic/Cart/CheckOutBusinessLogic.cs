using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.CartDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Newtonsoft.Json.Linq;
using Twilio.Rest.Api.V2010.Account;

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
                var voucherDao = CreateDao<VoucherDao>();
                var transactionDao = this.CreateDao<TransactionDao>();
                var today = DateTime.Now;
                var user = userDao.GetUserInfo(new GetOrderInfoInputDto()
                {
                    UserId = inputDto.CustomerId,
                });

                // voucher
                Voucher voucher = new Voucher();
                if (!string.IsNullOrEmpty(inputDto.Voucher))
                {
                    voucher = voucherDao.GetVoucherByCode(inputDto.Voucher);
                    if (voucher == null)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Voucher Invalid");
                    }
                    
                    if (voucher.ExpireDate <= today || voucher.EffectiveDate >= today)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Voucher Invalid");
                    }

                    if (voucherDao.CheckUsedVoucher(inputDto.CustomerId, voucher.VoucherId))
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You have already used this voucher");
                    }
                }

                if (!inputDto.PaymentMethod.Equals("wallet") && !inputDto.PaymentMethod.Equals("cod"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Your payment method invalid!");
                }

                if(inputDto.ListShop.Count == 0)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Your ListShop is empty!");
                }

                decimal totalPrice = 0;
                bool useVoucher = false;
                foreach (var item in inputDto.ListShop)
                {
                    if (item.CartItems.Count == 0)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Your CartItems of ShopId " + item.ShopId + " is empty!");
                    }

                    decimal shopPrice = 0;
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

                        shopPrice += food.Amount.Value * foodInfo.UnitPrice.Value;
                    }

                    if (item.ShopId.Equals(voucher.SellerId))
                    {
                        if (voucher.MinimumOrderValue > shopPrice)
                        {
                            return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Your order value of the shop " + item.ShopId + "is not enough!");
                        }

                        shopPrice -= voucher.DiscountAmount;
                        useVoucher = true;
                    }

                    totalPrice+= shopPrice;
                }
               
                // balance change

                if (totalPrice > user.Balance && inputDto.PaymentMethod.Equals("wallet"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Balance not enough!");
                }
                else if (totalPrice <= user.Balance && inputDto.PaymentMethod.Equals("wallet"))
                {
                    var input1 = new UpadateWalletBalanceDaoInputDto()
                    {
                        UserId = inputDto.CustomerId,
                        Value = -totalPrice,
                    };
                    var output1 = transactionDao.UpdateWalletBalance(input1);
                    if (!output1.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }

                    // Create Transaction
                    foreach (var item in inputDto.ListShop)
                    {
                        decimal shopPrice = 0;
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

                            shopPrice += food.Amount.Value * foodInfo.UnitPrice.Value;
                        }

                        if (useVoucher && item.ShopId.Equals(voucher.SellerId))
                        {
                            shopPrice -= voucher.DiscountAmount;
                        }

                        var input3 = new UpadateWalletBalanceDaoInputDto()
                        {
                            UserId = item.ShopId,
                            Value = totalPrice,
                        };
                        var output3 = transactionDao.UpdateWalletBalanceSeller(input3);
                        if (!output3.Success)
                        {
                            return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                        }

                        var input2 = new CreateTransaction()
                        {
                            UserId = inputDto.CustomerId,
                            RecieverId = item.ShopId,
                            TransactionType = 3,
                            Value = shopPrice,
                            Note = "Order food",
                            CreateDate = DateTime.Now,
                            Status = 1,
                        };

                        var output2 = transactionDao.Create(input2);
                        if (!output2.Success)
                        {
                            return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                        }
                    }
                }

                foreach (ListShopItemInputDto item in inputDto.ListShop)
                {
                    // create order + order detail
                    CheckOutOrderDaoInputDto orderdaoInput = mapper.Map<ListShopItemInputDto, CheckOutOrderDaoInputDto>(item);
                    if (item.ShopId.Equals(voucher.SellerId))
                    {
                        orderdaoInput.VoucherId = voucher.VoucherId;
                    }
                    orderdaoInput.CustomerId = inputDto.CustomerId;
                    orderdaoInput.ShipAddress = inputDto.ShipAddress;
                    orderdaoInput.Note = inputDto.Note;
                    orderdaoInput.CustomerPhone = inputDto.Phone;
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
                    // gen title and content notification
                    var notify = GenerateNotification.GetSingleton().GenNotificationNewOrderSeller(item.ShopId, (int)daoOutput.OrderId);
                    var noti = notifyDao.AddNewNotification(notify);
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
