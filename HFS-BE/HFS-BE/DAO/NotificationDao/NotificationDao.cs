using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Newtonsoft.Json.Linq;
using System;

namespace HFS_BE.DAO.NotificationDao
{
    public class NotificationDao : BaseDao
    {
        public NotificationDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AddNewNotification(List<NotificationAddNewInputDto> inputDto)
        {
            try
            {
                // gen notifyId
                var notify = context.Notifications
                    .OrderByDescending(x => x.Id)
                    .GroupBy(x => x.Id)
                    .Select(x => x.First())
                    
                    .ToList().FirstOrDefault();

                var notifyId = notify is null ? 1 : notify.Id + 1;

                foreach (var noti in inputDto)
                {
                    var inputModel = mapper.Map<NotificationAddNewInputDto, Notification>(noti);
                    inputModel.IsRead = false;
                    inputModel.Id = notifyId;
                    context.Add(inputModel);
                    context.SaveChanges();
                }

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public NotificationLst GetNotificationByReceiver(NotificationGetInput inputDto)
        {
            try
            {
                var query = context.Notifications
                    .Where(
                        x => x.Receiver.Equals(inputDto.Receiver) &&
                        x.Lang == LangTypeEnum.GetLangValue(inputDto.Lang)
                    )
                    .OrderByDescending(x => x.CreateDate)
                    .Select(x => new NotificationDaoOutputDto
                    {
                        Id = x.Id,
                        SendBy = x.SendBy,
                        Receiver = x.Receiver,
                        Type = NotificationTypeEnum.GetNotifyString(x.Type),
                        Title = x.Title,
                        Content = x.Content,
                        CreateDate = x.CreateDate.Value.ToString("MM/dd/yyyy hh:mm:ss tt"),
                        IsRead = x.IsRead
                    });

                if (inputDto.TakeNum != 0)
                {
                    query = query.Take(inputDto.TakeNum);
                }
                    
                var output = this.Output<NotificationLst>(Constants.ResultCdSuccess);
                //output.Posts = mapper.Map<List<Post>, List<PostOutputDto>>(data);
                output.Notifies = query.ToList();

                return output;
            }
            catch (Exception)
            {

                return Output<NotificationLst>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto UpdateReadNotification(NotificationReadInput inputDto)
        {
            try
            {
                var notifies = context.Notifications.Where(x => x.Id == inputDto.NotifyId).ToList();
                if(notifies == null || notifies.Count < 1)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Read fail", $"Notification Id : {inputDto.NotifyId} not exist");
                foreach(var noti in notifies)
                {
                    var notify = context.Notifications.FirstOrDefault(x => x.Id == noti.Id && x.Lang.Equals(x.Lang));
                    if (notify == null)
                        continue;
                    notify.IsRead = true;
                    context.SaveChanges();
                }
                
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto UpdateNotificationAllRead(NotificationInput inputDto)
        {
            try
            {
                // get all notify unread
                var output = context.Notifications
                    .Where(x => x.Receiver.Equals(inputDto.Receiver) && x.IsRead == false)
                    .ToList();
                // update all to readed
                foreach(var noti in output)
                {
                    UpdateReadNotification(new NotificationReadInput { NotifyId = noti.Id });
                }

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public NotificationOutputDto GetNotificationById(NotificationReadInput inputDto)
        {
            try
            {
                var result = context.Notifications.FirstOrDefault(x => x.Id == inputDto.NotifyId && x.Lang.Equals(inputDto.Lang));

                if (result == null)
                    return Output<NotificationOutputDto>(Constants.ResultCdFail,
                        "Read fail", $"Notify ID: {inputDto.NotifyId} not exist!");

                var output = Output<NotificationOutputDto>(Constants.ResultCdSuccess);
                output.Notify = mapper.Map<Notification, NotificationDaoOutputDto>(result);

                return output;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
