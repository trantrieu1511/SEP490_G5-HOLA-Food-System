using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.BusinessLogic.Notification
{
    public class AddNotificationBusinessLogic : BaseBusinessLogic
    {
        public AddNotificationBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AddNewNotification(NotificationAddNewInputDto inputDto)
        {
            try
            {
                var dao = CreateDao<NotificationDao>();
                return dao.AddNewNotification(inputDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
