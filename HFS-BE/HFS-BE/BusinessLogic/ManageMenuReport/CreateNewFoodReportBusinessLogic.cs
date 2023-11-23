using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.DAO.MenuReportDao;
using HFS_BE.DAO.ModeratorDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageMenuReport
{
    public class CreateNewFoodReportBusinessLogic : BaseBusinessLogic
    {
        public CreateNewFoodReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CreateNewFoodReport(CreateNewFoodReportInputDto inputDto, string reportBy, out string? sellerId)
        {
            sellerId = null;
            try
            {
                var dao = CreateDao<FoodReportDao>();
                var moderatorDao = CreateDao<ModeratorDao>();
                var notifyDao = CreateDao<NotificationDao>();
                var foodDao = CreateDao<FoodDao>();

                var output = dao.CreateNewFoodReport(inputDto, reportBy);
                if (!output.Success)
                {
                    return output;
                }

                // add notify
                // 1 get all food moderator
                var moderList = moderatorDao.GetAllMenuModerator();
                foreach (var moder in moderList.data)
                {
                    // 2. gen title and content notification
                    var notify = GenerateNotification.GetSingleton().GenNotificationFoodReport(moder.ModId, inputDto.FoodId, reportBy, inputDto.ReportContent);
                    //3. add notify
                    var noti = notifyDao.AddNewNotification(notify);
                    if (!noti.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }
                }

                // add noti for seller
                var food = foodDao.GetFoodById(inputDto.FoodId);
                sellerId = food.SellerId;
                var notify2 = GenerateNotification.GetSingleton().GenNotificationFoodReportSeller(food.SellerId, inputDto.FoodId, food.Name, inputDto.ReportContent);
                var noti2 = notifyDao.AddNewNotification(notify2);
                if (!noti2.Success)
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
