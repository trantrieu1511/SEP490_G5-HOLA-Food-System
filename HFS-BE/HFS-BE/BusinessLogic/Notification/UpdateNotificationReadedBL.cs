using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Notification
{
    public class UpdateNotificationReadedBL : BaseBusinessLogic
    {
        public UpdateNotificationReadedBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto UpdateNotificationReaded(NotificationReadedInput inputDto)
        {
            try
            {
                var dao = CreateDao<NotificationDao>();
                return dao.UpdateNotificationReaded(inputDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
