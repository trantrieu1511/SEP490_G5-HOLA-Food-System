using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class CancelOrderBusinessLogic : BaseBusinessLogic
    {
        public CancelOrderBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CancelOrder(OrderProgressCancelInputDto input, out string? customerId)
        {
            var oProgressDao = CreateDao<OrderProgressDao>();
            var orderDao = CreateDao<OrderDao>();
            var notifyDao = CreateDao<NotificationDao>();
            var transactionDao = this.CreateDao<TransactionDao>();

            // check orderid input
            var order = orderDao.GetOrderByOrderIdAndSellerId(input.OrderId, input.UserId);
            // put customerId
            customerId = order?.CustomerId;
            
            if (order == null)
                return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order is not exist");

            var getOrder = orderDao.GetOrderCustomer(new OrderCustomerDaoInputDto()
            {
                OrderId = input.OrderId,
                CustomerId = customerId,
            });
            if (getOrder == null)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Order not exsit!");
            }
            //get orderprogress by inputDto.orderId
            var orderProgresses = oProgressDao.GetOrderProgresByOrderId(input.OrderId);
            //check orderprogress exist or not
            if (orderProgresses.FirstOrDefault(x => x.Status == input.Status) != null)
                return Output<BaseOutputDto>(Constants.ResultCdFail, "Cancel Failed", $"OrderId: {input.OrderId} has been cancelled");

            var output = oProgressDao.AddOrderProgressCancelStatus(input);
            if (!output.Success)
            {
                return output;
            }

            // refund
            if (getOrder.PaymentMethod.Equals("Wallet"))
            {
                decimal? refund = getOrder.OrderDetails
                        .Select(d => d.UnitPrice * d.Quantity).ToList().Sum() - getOrder.VoucherDiscount;

                // create transaction:
                var input2 = new CreateTransaction()
                {
                    UserId = input.UserId,
                    RecieverId = customerId,
                    TransactionType = 4,
                    Value = refund,
                    Note = "Refund Order " + getOrder.OrderId + " cancel by seller!",
                    CreateDate = DateTime.Now,
                    Status = 1,
                };

                var output2 = transactionDao.Create(input2);

                // wallet balance change
                var input1 = new UpadateWalletBalanceDaoInputDto()
                {
                    UserId = customerId,
                    Value = refund,
                };
                var output1 = transactionDao.UpdateWalletBalance(input1);
                if (!output1.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }

                var input3 = new UpadateWalletBalanceDaoInputDto()
                {
                    UserId = input.UserId,
                    Value = -refund,
                };
                var output3 = transactionDao.UpdateWalletBalanceSeller(input3);
                if (!output3.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }
            }

            // 2. gen title and content notification
            var notifyBase = GenerateNotification.GetSingleton().GenNotificationCancelOrder(customerId, order.OrderId);
            //3. add notify
            var noti = notifyDao.AddNewNotification(notifyBase);
            if (!noti.Success)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }

            return Output<BaseOutputDto>(Constants.ResultCdSuccess);
        }
    }
}
