﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Food;
using HFS_BE.BusinessLogic.Post;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Food
{

    public class AddNewFoodController : BaseController
    {
        public AddNewFoodController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("foods/addNewFood")]
        public BaseOutputDto AddNewPost([FromForm] FoodCreateInputDto input)
        {
            try
            {

                var business = this.GetBusinessLogic<AddNewFoodBusinessLogic>();

                var inputBL = mapper.Map<FoodCreateInputDto, BusinessLogic.Food.FoodCreateInputDto>(input);

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