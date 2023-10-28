using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageFood
{
    public class UpdateFoodController : BaseController
    {
        public UpdateFoodController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPut("foods/updateFood")]
        [Authorize]
        public BaseOutputDto UpdateFood([FromForm] FoodUpdateInputDto input)
        {
            try
            { 
                var business = this.GetBusinessLogic<UpdateFoodBusinessLogic>();

                var inputBL = mapper.Map<FoodUpdateInputDto, BusinessLogic.ManageFood.FoodUpdateInputDto>(input);

                inputBL.UserDto = this.GetUserInfor();

                var output = business.UpdateFood(inputBL);

                // call signalR to Post Modelrator

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
