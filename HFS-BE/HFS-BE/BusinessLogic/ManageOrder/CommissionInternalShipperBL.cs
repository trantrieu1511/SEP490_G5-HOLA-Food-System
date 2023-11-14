using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class CommissionInternalShipperBL : BaseBusinessLogic
    {
        public CommissionInternalShipperBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CommissionInternal(OrderInternalShipInputDto input)
        {
            try
            {
                /*input.User.UserId = "SE00000001";*/

                var orderDao = CreateDao<OrderDao>();

                // check orderid input
                var order = orderDao.GetOrderByOrderIdAndSellerId(input.OrderId, input.User.UserId);
                if (order == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order is not exist");

                //check shipperId input
                var shipperDao = CreateDao<ShipperDao>();
                var shipper = shipperDao.GetShipperByShipperIdAndSellerId(input.User.UserId, input.ShipperId);
                if(shipper == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", $"ShipperId {input.ShipperId} is not work for shop");

                var outputAddInternal = orderDao.AddOrderInternalShipper(input);
                if (!outputAddInternal.Success)
                    return outputAddInternal;

                var oProgressDao = CreateDao<OrderProgressDao>();
                var inputInternal = mapper.Map<OrderInternalShipInputDto, OrderProgressStatusInputDto>(input);
                // set status Wait_Shipper = 2
                inputInternal.Status = 2;
                var outputAddOP = oProgressDao.AddOrderProgressCommonStatus(inputInternal);

                if(!outputAddOP.Success)
                    return outputAddOP;

                // add notification
                // gen title and content notification
                var notify = GenerateNotification.GetSingleton().GenNotificationInternalShipper(input.ShipperId, input.OrderId, input.User.Name);

                var notifyDao = CreateDao<NotificationDao>();
                var notifyOutput = notifyDao.AddNewNotification(notify);

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
