using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderHistoryBusinessLogic : BaseBusinessLogic
    {
        public OrderHistoryBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public OrderOnHistoryDaoOutputDto ListOrder(OrderHistoryInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<OrderDao>();
                
                return dao.OrderHistory(inputDto);
            }
            catch (Exception)
            {

                return this.Output<OrderOnHistoryDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
