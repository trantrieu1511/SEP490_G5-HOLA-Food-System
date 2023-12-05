using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.DAO.MenuReportDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Twilio.Rest.Api.V2010.Account;

namespace HFS_BE.BusinessLogic.ManageMenuReport
{
    public class ApproveNotApproveFoodReportBusinessLogic : BaseBusinessLogic
    {
        public ApproveNotApproveFoodReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto ApproveNotApproveFoodReport(ApproveNotApproveFoodReportInputDto inputDto, string updateBy, out string? sellerId)
        {
            sellerId = null;
            try
            {
                var dao = CreateDao<FoodReportDao>();
                var notifyDao = CreateDao<NotificationDao>();
                var foodDao = CreateDao<FoodDao>();

                var output = dao.ApproveNotApproveFoodReport(inputDto, updateBy);
                if (!output.Success)
                    return output;

                var food = foodDao.GetFoodById(inputDto.FoodId);

                if(food is null)
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);

                var notifyBase = inputDto.IsApproved ?
                    GenerateNotification.GetSingleton().GenNotificationApproveReport(inputDto.ReportBy, food.Name, inputDto.Note, 0) :
                    GenerateNotification.GetSingleton().GenNotificationNotApproveReport(inputDto.ReportBy, food.Name, inputDto.Note, 0);
                //3. add notify
                var noti = notifyDao.AddNewNotification(notifyBase);
                if (!noti.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }

                if(!inputDto.IsApproved)
                    return Output<BaseOutputDto>(Constants.ResultCdSuccess);

                // add noti for seller
                sellerId = food.SellerId;
                var notify2 = GenerateNotification.GetSingleton().GenNotificationFoodReportSeller(food.SellerId, inputDto.FoodId, food.Name, inputDto.Note);
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
