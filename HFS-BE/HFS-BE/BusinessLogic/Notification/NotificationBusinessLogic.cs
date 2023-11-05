using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.BusinessLogic.Notification
{
    public class NotificationBusinessLogic : BaseBusinessLogicSignalR
    {
        public NotificationBusinessLogic(SEP490_HFS_2Context context, IMapper mapper, IHubContext<Hub> hubContext) : base(context, mapper, hubContext)
        {
        }

        public BaseOutputDto SendNotification(NotificationAddNewInputDto inputDto)
        {
            try
            {

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
