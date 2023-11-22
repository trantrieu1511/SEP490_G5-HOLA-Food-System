using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageFood
{
    public class BanUnbanFoodController : BaseController
    {
        public BanUnbanFoodController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("foods/banunban")]
        [Authorize]
        public BaseOutputDto BanUnbanFood(FoodBanUnbanInputDto inputDto) {
            try
            {
                var business = GetBusinessLogic<BanUnbanFoodBusinessLogic>();
                return business.BanUnbanFood(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
