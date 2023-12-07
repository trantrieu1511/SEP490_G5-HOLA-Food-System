using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class CommissionExternalShipperBL : BaseBusinessLogic
    {
        public CommissionExternalShipperBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CommissionExternal(OrderExternalShipInputDto input)
        {
            try
            {
                /*input.UserId = "SE00000001";*/

                var orderDao = CreateDao<OrderDao>();

                // check orderid input
                var order = orderDao.GetOrderByOrderIdAndSellerId(input.OrderId, input.UserId);
                if (order == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order is not exist");
                // don't need update order 
                // next
                var oProgressDao = CreateDao<OrderProgressDao>();
                var inputExternal = mapper.Map<OrderExternalShipInputDto, OrderProgressStatusInputDto>(input);
                // set status Wait_Shipper = 2,
                inputExternal.Status = 2;
                // add new orderprpgress
                var outputAddOP = oProgressDao.AddOrderProgressCommonStatus(inputExternal);

                return outputAddOP;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
