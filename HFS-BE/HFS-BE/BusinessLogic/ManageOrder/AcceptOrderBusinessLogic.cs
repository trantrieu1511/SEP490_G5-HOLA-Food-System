using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Microsoft.AspNetCore.Components.Forms;
using static HFS_BE.Utils.Enum.OrderStatusEnum;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class AcceptOrderBusinessLogic : BaseBusinessLogic
    {
        public AcceptOrderBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AcceptOrder(OrderProgressStatusInputDto input, out string? customerId)
        {
            var oProgressDao = CreateDao<OrderProgressDao>();
            var orderDao = CreateDao<OrderDao>();
            var notifyDao = CreateDao<NotificationDao>();

            // check orderid input
            var order = orderDao.GetOrderByOrderIdAndSellerId(input.OrderId, input.UserId);

            // put customerId
            customerId = order?.CustomerId;
            //check order
            if (order == null)
                return Output<BaseOutputDto>(Constants.ResultCdFail, "Accept Failed", "Order is not exist");
            //check status order
            if(order.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Status == 6)
                return Output<BaseOutputDto>(Constants.ResultCdFail, "Accept Failed", "Order is canceled by customer");

            //get orderprogress by inputDto.orderId
            var data = oProgressDao.GetOrderProgresByOrderId(input.OrderId);
            // check trong list trả về có status = inputDto.status ko
            if (data.FirstOrDefault(x => x.Status == input.Status) != null)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }

            //add order progress
            var outputP = oProgressDao.AddOrderProgressCommonStatus(input);
            if (!outputP.Success)
            {
                return outputP;
            }

            // 2. gen title and content notification
            var notifyBase = GenerateNotification.GetSingleton().GenNotificationAcceptOrder(customerId, order.OrderId);
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
