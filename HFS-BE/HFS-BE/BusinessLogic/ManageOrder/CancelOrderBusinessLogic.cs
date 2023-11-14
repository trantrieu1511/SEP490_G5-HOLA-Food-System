using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.OrderProgressDao;
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

            // check orderid input
            var order = orderDao.GetOrderByOrderIdAndSellerId(input.OrderId, input.UserId);
            // put customerId
            customerId = order?.CustomerId;
            
            if (order == null)
                return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order is not exist");
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
