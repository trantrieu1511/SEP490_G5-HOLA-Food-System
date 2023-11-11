﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Notification;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Notification
{
    public class DisplayDetailNotificationController : BaseController
    {
        public DisplayDetailNotificationController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("notifies/getDetailNotify")]
        public NotificationOutputDto DisplayDetailNotificationById([FromQuery]NotificationReadInput inputDto)
        {
            try
            {
                var busi = GetBusinessLogic<DisplayDetailNotificationBusinessLogic>();
                return busi.DisplayDetailNotificationById(inputDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}