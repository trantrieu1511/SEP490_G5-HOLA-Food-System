using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageFood
{
    public class EnableDisableFoodController : BaseController
    {
        public EnableDisableFoodController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("foods/enableDisable")]
        [Authorize]
        public BaseOutputDto EnableDisableFood(FoodEnableDisableInputDto input)
        {
            try
            {

                var business = this.GetBusinessLogic<EnableDisableFoodBusinessLogic>();

                var output = business.EnableDisableFood(input);

                // call signalR 

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
