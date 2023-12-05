using AutoMapper;
using CloudinaryDotNet.Actions;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePost
{
    public class BanUnbanPostBusinessLogic : BaseBusinessLogic
    {
        public BanUnbanPostBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto BanUnbanPost(PostBanUnbanInputDto inputDto, string userId, out string? sellerId) {
            sellerId = null;
            try
            {
                var postDao = CreateDao<PostDao>();
                var notifyDao = CreateDao<NotificationDao>();

                var postResult = postDao.BanUnbanPost(inputDto, userId);
                if (!postResult.Success)
                    return postResult;

                var post = postDao.GetPostById(inputDto.PostId);
                sellerId = post.SellerId;
                var notifyBase = inputDto.isBanned ?
                    GenerateNotification.GetSingleton().GenNotificationBanPost(post.SellerId, post.PostId, post.BanNote) :
                    GenerateNotification.GetSingleton().GenNotificationUnBanPost(post.SellerId, post.PostId);
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
