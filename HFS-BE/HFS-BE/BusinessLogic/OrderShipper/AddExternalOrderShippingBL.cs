using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Controllers.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class AddExternalOrderShippingBL : BaseBusinessLogic
    {
        public AddExternalOrderShippingBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AddExternalShipping(ExternalShippingOrderBL inputDto, out string? sellerId,out string? customerId)
        {
            sellerId = null;
            customerId = null;

            try
            {
                inputDto.UserDto.UserId = "SH00000001";

                var orderDao = CreateDao<OrderDao>();
                var order = orderDao.GetOrderByOrderId(inputDto.OrderId);
                //check order
                if (order == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order is not exist");
                //check order status
                if(order.Status != Constants.OrderExternal)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order is not external for ship");
                // check order is wait shipper or not
                var progressDao = CreateDao<OrderProgressDao>();
                var progress = progressDao.GetOrderProgresByOrderId(order.OrderId);
                var statusLast = progress.OrderByDescending(y => y.CreateDate).FirstOrDefault().Status;
                if(statusLast != Constants.Wait_Shipper)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order does not accept shippers");
                }
                //check shipper has been add or not
                if (order.ShipperId != null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order has been delivered to the shipper");

                sellerId = order.SellerId;
                customerId = order.CustomerId;

                // add external shipperId 
                var outputAdd = orderDao.AddOrderExternalShipper(new OrderExternalShipInputDto
                {
                    OrderId = inputDto.OrderId,
                    UserId = inputDto.UserDto.UserId
                });

                var outputP = progressDao.AddNewOrderProgress(new OrderProgress
                {
                    Note = "External Shipping",
                    CreateDate = DateTime.Now,
                    OrderId = order.OrderId,
                    Status = Constants.Shipping,
                    ShipperId = inputDto.UserDto.UserId
                });

                if (!outputP.Success)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Add Order Failed");
                }

                //gen title and content notification
                var notifyCus = GenerateNotification.GetSingleton().GenNotificationOrderShipping(customerId, order.OrderId);
                var notifySell = GenerateNotification.GetSingleton().GenNotificationOrderShipping(order.SellerId, order.OrderId);

                var notifyDao = CreateDao<NotificationDao>();
                //add notify
                var notiCus = notifyDao.AddNewNotification(notifyCus);
                var notiSel = notifyDao.AddNewNotification(notifySell);

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
            
        }
    }
}
