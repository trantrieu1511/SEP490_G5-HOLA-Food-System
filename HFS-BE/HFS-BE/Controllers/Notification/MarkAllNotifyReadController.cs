using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Notification;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Notification
{
    public class MarkAllNotifyReadController : BaseController
    {
        public MarkAllNotifyReadController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("notifies/markAllRead")]
        [Authorize]
        public BaseOutputDto MarkAllRead()
        {
            try
            {
                var busi = GetBusinessLogic<MarkAllNotificationReadBL>();
                return busi.MarkAllRead(new NotificationInput { Receiver = GetUserInfor().UserId});
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
