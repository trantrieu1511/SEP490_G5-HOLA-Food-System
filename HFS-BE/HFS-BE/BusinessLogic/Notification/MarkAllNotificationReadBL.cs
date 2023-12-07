using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Notification
{
    public class MarkAllNotificationReadBL : BaseBusinessLogic
    {
        public MarkAllNotificationReadBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto MarkAllRead(NotificationInput inputDto)
        {
            try
            {
                var dao = CreateDao<NotificationDao>();
                return dao.UpdateNotificationAllRead(inputDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
