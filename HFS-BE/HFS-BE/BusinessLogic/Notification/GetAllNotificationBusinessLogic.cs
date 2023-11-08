using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Notification
{
    public class GetAllNotificationBusinessLogic : BaseBusinessLogic
    {
        public GetAllNotificationBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public NotificationLst GetNotificationByReceiver(NotificationInput inputDto)
        {
            try
            {
                var dao = CreateDao<NotificationDao>();
                return dao.GetNotificationByReceiver(inputDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
