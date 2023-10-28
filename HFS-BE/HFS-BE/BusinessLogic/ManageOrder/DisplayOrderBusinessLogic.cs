using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class DisplayOrderBusinessLogic : BaseBusinessLogic
    {
        public DisplayOrderBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public OrderSellerDaoOutputDto ListOrder(OrderSellerByStatusInputDto inputDto)
        {
            inputDto.UserId = 1;

            // get orders
            var orderDao = CreateDao<OrderDao>();
            var orders = orderDao.GetOrderByStatus(inputDto);
            //map dao output -> BL ouput
            var ordersMapper = mapper.Map<Dao.OrderDao.OrderSellerDaoOutputDto, OrderSellerDaoOutputDto>(orders);
            //convert image DB to Base64
            

            return null;
        }
    }
}
