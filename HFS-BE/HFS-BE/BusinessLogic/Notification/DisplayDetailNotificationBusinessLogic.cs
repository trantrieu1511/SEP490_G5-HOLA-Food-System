using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Notification
{
    public class DisplayDetailNotificationBusinessLogic : BaseBusinessLogic
    {
        public DisplayDetailNotificationBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public NotificationOutputDto DisplayDetailNotificationById(NotificationReadInput inputDto)
        {
            try
            {
                var dao = CreateDao<NotificationDao>();
                // update notify isRead = true
                var outputUpdate = dao.UpdateReadNotification(inputDto);
                if (!outputUpdate.Success)
                    return Output<NotificationOutputDto>(Constants.ResultCdFail, outputUpdate.Message, outputUpdate.Errors.SystemErrors.ToList().First());
                // get notificaiton detail

                return dao.GetNotificationById(inputDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
