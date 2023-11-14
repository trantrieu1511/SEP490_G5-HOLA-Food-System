using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Controllers.ManageFood
{

    public class AddNewFoodController : BaseControllerSignalR
    {
        public AddNewFoodController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("foods/addNewFood")]
        [Authorize]

        public async Task<BaseOutputDto> AddNewFood([FromForm] FoodCreateInputDto input)
        {
            try
            {

                var business = this.GetBusinessLogic<AddNewFoodBusinessLogic>();

                var inputBL = mapper.Map<FoodCreateInputDto, BusinessLogic.ManageFood.FoodCreateInputDto>(input);

                inputBL.UserDto = this.GetUserInfor();

                var output = business.AddNewFood(inputBL);

                // call signalR to Food Modelrator
                if (output.Success)
                {
                    //notify for all Food Modelrator
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    // refresh data of all Food Modelrator
                    var dataRealTimeHub = _hubContextFactory.CreateHub<DataRealTimeHub>();

                    await notifyHub.Clients.All.SendAsync("foodNotification");
                    await dataRealTimeHub.Clients.All.SendAsync("foodDataRealTime");
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
