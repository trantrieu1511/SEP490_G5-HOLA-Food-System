using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.Extensions.Hosting;

namespace HFS_BE.BusinessLogic.ManageOrderCustomer
{
    public class FeedBackFoodBusinessLogic : BaseBusinessLogic
    {
        public FeedBackFoodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto FeedBackFood(CreateFeedBackDaoInputDto inputDto, out string? sellerId)
        {
            sellerId = null;
            try
            {
                var orderDao = this.CreateDao<OrderDao>();
                var feedBackDao = this.CreateDao<FeedBackDao>();
                var foodDao = CreateDao<FoodDao>();
                var notifyDao = CreateDao<NotificationDao>();

                var getOrder = orderDao.GetOrderCustomerFoodId(new GetOrdersCustomerFoodIdDaoInputDto()
                {
                    CustomerId = inputDto.CustomerId,
                    FoodId = inputDto.FoodId,
                });

                if (!getOrder.ListOrders.Any() || getOrder.ListOrders.FirstOrDefault(x => x.Status == 4) == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You have never ordered this dish before!");
                }

                var output = feedBackDao.CreateFeedBack(inputDto);
                if (!output.Success)
                {
                    return output;
                }

                var food = foodDao.GetFoodById((int)inputDto.FoodId);

                sellerId = food.SellerId;

                var notifyBase = GenerateNotification.GetSingleton().GenNotificationOrderFeedBack(sellerId, food.FoodId);
                //3. add notify
                var noti = notifyDao.AddNewNotification(notifyBase);
                if (!noti.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);


            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
