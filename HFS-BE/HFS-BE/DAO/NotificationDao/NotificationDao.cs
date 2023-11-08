using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Newtonsoft.Json.Linq;
using System;

namespace HFS_BE.DAO.NotificationDao
{
    public class NotificationDao : BaseDao
    {
        public NotificationDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AddNewNotification(NotificationAddNewInputDto inputDto)
        {
            try
            {
                var inputModel = mapper.Map<NotificationAddNewInputDto, Notification>(inputDto);
                inputModel.IsRead = false;
                context.Add(inputModel);
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public NotificationLst GetNotificationByReceiver(NotificationInput inputDto)
        {
            try
            {
                var notifies = context.Notifications
                    .Where(x => x.Receiver.Equals(inputDto.Receiver))
                    .OrderByDescending(x => x.CreateDate)
                    .Take(5)
                    .Select(x => new NotificationDaoOutputDto
                    {
                        Id = x.Id,
                        SendBy = x.SendBy,
                        Receiver = x.Receiver,
                        TypeId = x.TypeId,
                        TypeName = x.Type.Name,
                        Title = x.Title,
                        Content = x.Content,
                        CreateDate = x.CreateDate.Value.ToString("MM/dd/yyyy hh:mm:ss tt"),
                        IsRead = x.IsRead
                    })
                    .ToList();

                var output = this.Output<NotificationLst>(Constants.ResultCdSuccess);
                //output.Posts = mapper.Map<List<Post>, List<PostOutputDto>>(data);
                output.Notifies = notifies;

                return output;
            }
            catch (Exception)
            {

                return Output<NotificationLst>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto UpdateNotificationReaded(NotificationReadedInput inputDto)
        {
            try
            {
                var notify = context.Notifications.FirstOrDefault(x => x.Id == inputDto.NotifyId);
                if(notify == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Read fail", $"Notification Id : {inputDto.NotifyId} not exist");
                notify.IsRead = true;
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
