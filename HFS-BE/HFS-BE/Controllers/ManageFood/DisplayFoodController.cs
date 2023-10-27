using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageFood
{
    public class DisplayFoodController : BaseController
    {
        public DisplayFoodController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("foods/getFoodsSeller")]
        public ListFoodOutputSellerDto Get()
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayFoodBusinessLogic>();

                return business.ListFoods(this.GetUserInfor());
            }
            catch (Exception)
            {
                return this.Output<ListFoodOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}
