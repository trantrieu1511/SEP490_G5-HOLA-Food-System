using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using static HFS_BE.Utils.Enum.OrderProgressStatusEnum;

namespace HFS_BE.BusinessLogic.ManageOrderCustomer
{
    public class CancelOrderBusinessLogic : BaseBusinessLogic
    {
        public CancelOrderBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CancelOrder(OrderCustomerDaoInputDto inputDto, out string? sellerId)
        {
            sellerId = null;
            try
            {
                var dao = this.CreateDao<OrderDao>();
                var orderProgressDao = this.CreateDao<OrderProgressDao>();
                var transactionDao = this.CreateDao<TransactionDao>();
                var notifyDao = CreateDao<NotificationDao>();

                var getOrder = dao.GetOrderCustomer(inputDto);
                if (getOrder == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Order not exsit!");
                }
                
                if (getOrder.Status == 3 || getOrder.Status == 4 || getOrder.Status == 5)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Can't Cancel order!");
                }

                var updateOrder = dao.CancelOrderCustomer(inputDto);
                var orderProgressInput = new OrderCreateDaoInputDto()
                {
                    OrderId = inputDto.OrderId,
                    UserId = inputDto.CustomerId,
                    Status = 6,
                    CreateDate = DateTime.Now,
                    Note = inputDto.Note,
                };

                // refund
                if (getOrder.PaymentMethod.Equals("Wallet"))
                {
                    decimal? refund = getOrder.OrderDetails
                        .Select(d => d.UnitPrice * d.Quantity).ToList().Sum() - getOrder.VoucherDiscount;

                    // create transaction:
                    var input2 = new CreateTransaction()
                    {
                        UserId = getOrder.SellerId,
                        RecieverId = inputDto.CustomerId,
                        TransactionType = 4,
                        Value = refund,
                        Note = "Refund Order " + getOrder.OrderId,
                        CreateDate = DateTime.Now,
                        Status = 1,
                    };

                    var output2 = transactionDao.Create(input2);

                    // wallet balance change
                    var input1 = new UpadateWalletBalanceDaoInputDto()
                    {
                        UserId = inputDto.CustomerId,
                        Value = refund,
                    };
                    var output1 = transactionDao.UpdateWalletBalance(input1);
                    if (!output1.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }

                    // wallet balance change
                    var input3 = new UpadateWalletBalanceDaoInputDto()
                    {
                        UserId = getOrder.SellerId,
                        Value = -refund,
                    };
                    var output3 = transactionDao.UpdateWalletBalanceSeller(input3);
                    if (!output1.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }
                }
                sellerId = getOrder.SellerId;
                var notifyBase = GenerateNotification.GetSingleton().GenNotificationOrderCustomerCancel(sellerId, inputDto.OrderId, inputDto.Note);
                //3. add notify
                var noti = notifyDao.AddNewNotification(notifyBase);
                if (!noti.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }

                return orderProgressDao.CreateOrderProgressCustomer(orderProgressInput);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
