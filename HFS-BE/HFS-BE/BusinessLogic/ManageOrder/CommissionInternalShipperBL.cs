using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;
using HFS_BE.Utils;

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
                input.UserId = "SE00000001";

                var orderDao = CreateDao<OrderDao>();

                // check orderid input
                var order = orderDao.GetOrderByOrderIdAndSellerId(input.OrderId, input.UserId);
                if (order == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order is not exist");

                //check shipperId input
                var shipperDao = CreateDao<ShipperDao>();
                var shipper = shipperDao.GetShipperByShipperIdAndSellerId(input.UserId, input.ShipperId);
                if(shipper == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", $"ShipperId {input.ShipperId} is not work for shop");

                var outputAddInternal = orderDao.AddOrderInternalShipper(input);
                if (!outputAddInternal.Success)
                    return outputAddInternal;

                var oProgressDao = CreateDao<OrderProgressDao>();
                var inputInternal = mapper.Map<OrderInternalShipInputDto, OrderProgressStatusInputDto>(input);
                var outputAddOP = oProgressDao.AddOrderProgressCommonStatus(inputInternal);

                return outputAddOP;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
