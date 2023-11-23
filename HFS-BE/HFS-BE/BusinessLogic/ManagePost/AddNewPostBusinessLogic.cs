using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.ModeratorDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace HFS_BE.BusinessLogic.ManagePost
{
    public class AddNewPostBusinessLogic : BaseBusinessLogic
    {
        public AddNewPostBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AddNewPost(PostCreateInputDto inputDto)
        {
            try
            {
                var notifyDao = CreateDao<NotificationDao>();
                var moderatorDao = CreateDao<ModeratorDao>();

                if (String.IsNullOrEmpty(inputDto.PostContent))
                {
                    var errors = new List<string>();
                    errors.Add("PostContent cannot be empty!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", errors);
                    
                }

                if(inputDto.PostContent.Length > Constants.PostContentMaxLength)
                {
                    var errors = new List<string>();
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Post content must not exceed 1500 characters");
                }

                var Dao = this.CreateDao<PostDao>();
                var inputMapper = mapper.Map<PostCreateInputDto, Dao.PostDao.PostCreateInputDto>(inputDto);

                // save file to server -> return list file name
                var fileNames = new List<string>();
                if (inputDto.Images != null && inputDto.Images.Count > 0)
                {
                    fileNames = ReadSaveImage.SaveImages(inputDto.Images, inputDto.UserDto, 0);
                    inputMapper.Images = fileNames;
                }
                else
                {
                    inputMapper.Images = null;
                }

                var output = Dao.AddNewPost(inputMapper);

                if (!output.Success)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", output.Message);

                // add notify
                // 1 get all food moderator
                var moderList = moderatorDao.GetAllPostModerator();
                foreach (var moder in moderList.data)
                {
                    // 2. gen title and content notification
                    var notify = GenerateNotification.GetSingleton().GenNotificationAddNewPost(moder.ModId, output.PostId);
                    //3. add notify
                    var noti = notifyDao.AddNewNotification(notify);
                    if (!noti.Success)
                    {
                        return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                    }
                }

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
