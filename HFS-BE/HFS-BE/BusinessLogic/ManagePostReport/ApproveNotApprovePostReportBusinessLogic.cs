using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Twilio.Rest.Api.V2010.Account;

namespace HFS_BE.BusinessLogic.ManagePostReport
{
    public class ApproveNotApprovePostReportBusinessLogic : BaseBusinessLogic
    {
        public ApproveNotApprovePostReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto ApproveNotApprovePostReport(ApproveNotApprovePostReportInputDto inputDto, string updateBy, out string? sellerId)
        {
            sellerId = null;
            try
            {
                var dao = CreateDao<PostReportDao>();
                var notifyDao = CreateDao<NotificationDao>();
                var postDao = CreateDao<PostDao>();

                var output = dao.ApproveNotApprovePostReport(inputDto, updateBy);
                if (!output.Success)
                    return output;

                var post = postDao.GetPostById(inputDto.PostId);

                if (post is null)
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);

                var notifyBase = inputDto.IsApproved ?
                    GenerateNotification.GetSingleton().GenNotificationApproveReport(inputDto.ReportBy, post.Seller.ShopName, inputDto.Note, 1) :
                    GenerateNotification.GetSingleton().GenNotificationNotApproveReport(inputDto.ReportBy, post.Seller.ShopName, inputDto.Note, 1);
                //3. add notify
                var noti = notifyDao.AddNewNotification(notifyBase);
                if (!noti.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }

                /*if (!inputDto.IsApproved)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }

                // add post for seller
                sellerId = post.SellerId;
                var notify2 = GenerateNotification.GetSingleton().GenNotificationPostReportSeller(post.SellerId, inputDto.PostId, inputDto.Note);
                var noti2 = notifyDao.AddNewNotification(notify2);

                if (!noti2.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }*/

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
