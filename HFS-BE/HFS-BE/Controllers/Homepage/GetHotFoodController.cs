using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Homepage;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;
using Twilio.Rest.Trunking.V1;

namespace HFS_BE.Controllers.Homepage
{
    public class GetHotFoodController : BaseController
    {
        public GetHotFoodController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("home/hotfoods")]
        public FoodShopDaoOutputDto GetHotFood()
        {
            try
            {
                var busi = this.GetBusinessLogic<GetHotFoodBusinessLogic>();
                return busi.GetHotFoods();
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
