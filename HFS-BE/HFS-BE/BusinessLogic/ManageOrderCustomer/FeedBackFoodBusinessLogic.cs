using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageOrderCustomer
{
    public class FeedBackFoodBusinessLogic : BaseBusinessLogic
    {
        public FeedBackFoodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto FeedBackFood(CreateFeedBackDaoInputDto inputDto)
        {
            try
            {
                var orderDao = this.CreateDao<OrderDao>();
                var feedBackDao = this.CreateDao<FeedBackDao>();

                var getOrder = orderDao.GetOrderCustomerFoodId(new GetOrdersCustomerFoodIdDaoInputDto()
                {
                    CustomerId = inputDto.CustomerId,
                    FoodId = inputDto.FoodId,
                });

                if (!getOrder.ListOrders.Any() || getOrder.ListOrders.FirstOrDefault(x => x.Status == 4) == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You have never ordered this dish before!");
                }

                return feedBackDao.CreateFeedBack(inputDto);


            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
