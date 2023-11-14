using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Notification;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Notification
{
    public class UpdateReadNotifyController : BaseController
    {
        public UpdateReadNotifyController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("notifies/updateNotify")]
        [Authorize]
        public BaseOutputDto UpdateNotificationReaded(NotificationReadInput inputDto)
        {
            try
            {
                var dao = GetBusinessLogic<UpdateReadNotificationBL>();
                return dao.UpdateNotificationReaded(inputDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
