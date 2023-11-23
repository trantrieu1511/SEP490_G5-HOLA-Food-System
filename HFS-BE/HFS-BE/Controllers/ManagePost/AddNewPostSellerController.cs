using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace HFS_BE.Controllers.ManagePost
{

    public class AddNewPostSellerController : BaseControllerSignalR
    {
        public AddNewPostSellerController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("posts/addPostSeller")]
        [Authorize]
        public async Task<BaseOutputDto> AddNewPost([FromForm] IReadOnlyList<IFormFile> images, [FromForm] string postContent)
        {
            try
            {
                /*var formCollection = Request.Form;

                // Access the uploaded files
                var files = formCollection.Files;
                var files = formCollection.Files.GetFiles("images");
                var postContent1 = formCollection["postContent"];*/

                var business = this.GetBusinessLogic<AddNewPostBusinessLogic>();

                PostCreateInputDto inputBL = new PostCreateInputDto
                {
                    Images = images,
                    PostContent = postContent,
                };

                inputBL.UserDto = this.GetUserInfor();

                var output = business.AddNewPost(inputBL);

                // call signalR to Post Modelrator

                if (output.Success)
                {
                    var dataRealTimeHub = _hubContextFactory.CreateHub<DataRealTimeHub>();
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();

                    await notifyHub.Clients.All.SendAsync("postNotification");
                    await dataRealTimeHub.Clients.All.SendAsync("postDataRealTime");
                    //await this._hubContextFactory.Clients.All.SendAsync("dataRealTime");
                }

                return output;
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}
