using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.Dao.OrderDao
{
    public class OrderDao : BaseDao
    {
        public OrderDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public OrderByShipperDaoOutputDto OrderByShipper(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                var data = this.context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                    .Where(x => x.ShipperId == inputDto.ShipperId)
                    .Select(x => mapper.Map<Order, OrderDaoOutputDto>(x));
                var output = this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdSuccess);
                output.OrderList = data.ToList();
                return output;
            }
            catch (Exception)
            {

                return this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdFail);
            }
        }
        public OrderHistoryDetailDtoOutput OrderHistoryDetail(int orderId)
        {
            try
            {
                var data = this.context.Orders.
                    Include(x => x.OrderProgresses).
                    Include(x => x.OrderDetails).ThenInclude(x => x.Food)

                    .Where(x => x.OrderId == orderId)
                    .Select(x => mapper.Map<Order, OrderHistoryDetailDtoOutput>(x)).FirstOrDefault();
                var output = this.Output<OrderHistoryDetailDtoOutput>(Constants.ResultCdSuccess);
                output = data;
                return output;
            }
            catch (Exception)
            {

                return this.Output<OrderHistoryDetailDtoOutput>(Constants.ResultCdFail);
            }
        }

        public OrderOnHistoryDaoOutputDto OrderHistory(OrderHistoryInputDto inputDto)
        {
            try
            {
                var data = this.context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                    .Where(x => x.ShipperId == inputDto.ShipperId && x.Status == inputDto.Status)
                    .Select(x => mapper.Map<Order, OrderDaoOutputDto>(x));
                var output = this.Output<OrderOnHistoryDaoOutputDto>(Constants.ResultCdSuccess);
                output.OrderList = data.ToList();
                return output;
            }
            catch (Exception)
            {

                return this.Output<OrderOnHistoryDaoOutputDto>(Constants.ResultCdFail);
            }
        }
        public CheckOutOrderDaoOutputDto CheckOutOrder(CheckOutOrderDaoInputDto inputDto)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                return this.Output<CheckOutOrderDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
