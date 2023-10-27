using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Controllers.ManageFood
{

    public class AddNewFoodController : BaseController
    {
        public AddNewFoodController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("foods/addNewFood")]

        public BaseOutputDto AddNewPost([FromForm] FoodCreateInputDto input)
        {
            try
            {

                var business = this.GetBusinessLogic<AddNewFoodBusinessLogic>();

                var inputBL = mapper.Map<FoodCreateInputDto, BusinessLogic.ManageFood.FoodCreateInputDto>(input);

                inputBL.UserDto = this.GetUserInfor();

                var output = business.AddNewFood(inputBL);

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
