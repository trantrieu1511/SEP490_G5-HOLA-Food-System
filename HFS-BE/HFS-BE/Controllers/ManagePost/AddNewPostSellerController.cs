﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePost
{

    public class AddNewPostSellerController : BaseController
    {
        public AddNewPostSellerController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("posts/addPostSeller")]
        [Authorize]
        public BaseOutputDto AddNewPost([FromForm] IReadOnlyList<IFormFile> images, [FromForm] string postContent)
        {
            try
            {
                /*var formCollection = Request.Form;

                // Access the uploaded files
                var files = formCollection.Files;
                var files = formCollection.Files.GetFiles("images");
                var postContent1 = formCollection["postContent"];*/

                var business = this.GetBusinessLogic<AddNewPostBusinessLogic>();

                BusinessLogic.ManagePost.PostCreateInputDto inputBL = new BusinessLogic.ManagePost.PostCreateInputDto
                {
                    Images = images,
                    PostContent = postContent,
                };

                inputBL.UserDto = this.GetUserInfor();

                var output = business.AddNewPost(inputBL);

                // call signalR to Post Modelrator

                return output;
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}
