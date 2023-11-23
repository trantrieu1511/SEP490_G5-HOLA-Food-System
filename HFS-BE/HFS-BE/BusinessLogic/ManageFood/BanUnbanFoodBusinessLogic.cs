using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.Extensions.Hosting;

namespace HFS_BE.BusinessLogic.ManageFood
{
    public class BanUnbanFoodBusinessLogic : BaseBusinessLogic
    {
        public BanUnbanFoodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto BanUnbanFood(FoodBanUnbanInputDto inputDto, string userId, out string? sellerId) {
            sellerId = null;
            try
            {
                var dao = CreateDao<FoodDao>();
                var notifyDao = CreateDao<NotificationDao>();

                var output = dao.BanUnbanFood(inputDto, userId);
                if (!output.Success)
                {
                    return output;
                }

                var food = dao.GetFoodById(inputDto.FoodId);

                sellerId = food.SellerId;
                var notifyBase = inputDto.isBanned ?
                GenerateNotification.GetSingleton().GenNotificationBanFood(food.SellerId, food.FoodId) :
                    GenerateNotification.GetSingleton().GenNotificationUnBanFood(food.SellerId, food.FoodId);
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
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
