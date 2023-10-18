﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.Post
{
    public class AddNewPostBusinessLogic : BaseBusinessLogic
    {
        public AddNewPostBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AddNewPost(PostCreateInputDto inputDto)
        {
            try
            {
                inputDto.UserDto = new UserDto
                {
                    Email = "test@gmail.com",
                    Name = "testSeller",
                    RoleId = 1,
                    UserId = 1,
                };
                // save file to server -> return list file name
                List<string> fileNames = ReadSaveImage.SaveImages(inputDto.Images, inputDto.UserDto, 0);

                var Dao = this.CreateDao<PostDao>();
                var inputMapper = mapper.Map<PostCreateInputDto, Dao.PostDao.PostCreateInputDto>(inputDto);
                inputMapper.Images = fileNames;

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