using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.ModeratorDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePostReport
{
    public class CreateNewPostReportBusinessLogic : BaseBusinessLogic
    {
        public CreateNewPostReportBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CreateNewPostReport(CreateNewPostReportInputDto inputDto, string reportBy, out string? sellerId)
        {
            sellerId = null;
            try
            {
                var dao = CreateDao<PostReportDao>();
                var moderatorDao = CreateDao<ModeratorDao>();
                var notifyDao = CreateDao<NotificationDao>();
                var postDao = CreateDao<PostDao>();

                var output = dao.CreateNewPostReport(inputDto, reportBy);
                if (!output.Success)
                {
                    return output;
                }

                // add notify
                // 1 get all food moderator
                var moderList = moderatorDao.GetAllPostModerator();
                foreach (var moder in moderList.data)
                {
                    // 2. gen title and content notification
                    var notify = GenerateNotification.GetSingleton().GenNotificationPostReport(moder.ModId, inputDto.PostId, reportBy, inputDto.ReportContent);
                    //3. add notify
                    var noti = notifyDao.AddNewNotification(notify);
                    if (!noti.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }
                }

                var post = postDao.GetPostById(inputDto.PostId);
                sellerId = post.SellerId;
                var notify2 = GenerateNotification.GetSingleton().GenNotificationPostReportSeller(post.SellerId, inputDto.PostId, inputDto.ReportContent);
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
