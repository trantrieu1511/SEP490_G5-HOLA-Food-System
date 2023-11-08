using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.FoodDetail;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.FoodDetail
{
    public class GetFoodByCategoryController : BaseController
    {
        public GetFoodByCategoryController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("fooddetail/similarfood")]
        public FoodShopDaoOutputDto GetFoodByCate(GetFoodByCategoryDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<GetFoodByCategoryBusinessLogic>();
                return busi.GetFoodByCategory(inputDto);
            }
            catch (Exception)
            {
                return this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
