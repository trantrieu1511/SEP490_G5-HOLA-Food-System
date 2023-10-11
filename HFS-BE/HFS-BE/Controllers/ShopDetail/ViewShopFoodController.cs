using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Menu;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Ultis;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ShopDetail
{
    public class ViewShopFoodController : BaseController
    {
        public ViewShopFoodController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("shopdetail")]
        public FoodShopDaoOutputDto DisplayFood(FoodByShopDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<ViewShopFoodBusinessLogic>();
                var a = this.GetAccessRight();
                return busi.FoodShop(inputDto);
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
