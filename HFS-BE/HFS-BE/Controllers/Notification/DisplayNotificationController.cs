﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Notification;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Notification
{
    public class DisplayNotificationController : BaseController
    {
        public DisplayNotificationController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("notifies/getAllNotify")]
        [Authorize]
        public NotificationLst GetNotificationByReceiver()
        {
            try
            {
                NotificationInput input = new NotificationInput
                {
                    Receiver = GetUserInfor().UserId
                };

                var business = GetBusinessLogic<GetAllNotificationBusinessLogic>();
                return business.GetNotificationByReceiver(input);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}