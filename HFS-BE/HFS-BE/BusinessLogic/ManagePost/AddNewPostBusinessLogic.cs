using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
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
                /*inputDto.UserDto = new UserDto
                {
                    Email = "test@gmail.com",
                    Name = "testSeller",
                    RoleId = 2,
                    UserId = 1,
                };*/

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

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
