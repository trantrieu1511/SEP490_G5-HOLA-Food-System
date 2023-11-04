using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;

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
                context.Add(inputModel);
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
