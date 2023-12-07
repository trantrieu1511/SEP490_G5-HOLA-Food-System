using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Notification
{
    public class UpdateReadNotificationBL : BaseBusinessLogic
    {
        public UpdateReadNotificationBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto UpdateNotificationReaded(NotificationReadInput inputDto)
        {
            try
            {
                var dao = CreateDao<NotificationDao>();
                return dao.UpdateReadNotification(inputDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
