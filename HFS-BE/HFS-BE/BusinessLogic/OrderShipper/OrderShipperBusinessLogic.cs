using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderShipperBusinessLogic : BaseBusinessLogic
    {
        public OrderShipperBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public OrderByShipperDaoOutputDto ListOrderShipper(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<OrderDao>();
                return dao.OrderByShipper(inputDto);
            }
            catch (Exception)
            {

                return this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdFail);
            }
        }
        public OrderHistoryDetailDtoOutput ListOrder(int orderId)
        {
            try
            {
                var dao = this.CreateDao<OrderDao>();
                return dao.OrderHistoryDetail(orderId);
            }
            catch (Exception)
            {

                return this.Output<OrderHistoryDetailDtoOutput>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto ChangeStatus(OrderStatusInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<OrderDao>();
                return dao.ChangeStatusOrderShipper(inputDto);
            }
            catch (Exception)
            {

                return this.Output<OrderHistoryDetailDtoOutput>(Constants.ResultCdFail);
            }
        }
    }
}
