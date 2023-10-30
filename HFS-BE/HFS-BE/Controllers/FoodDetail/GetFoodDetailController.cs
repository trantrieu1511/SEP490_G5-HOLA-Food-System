using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.FoodDetail;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.FoodDetail
{
    public class GetFoodDetailController : BaseController
    {
        public GetFoodDetailController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost]
        [Route("fooddetail")]
        public FoodOutputDto GetFoodDetail(GetFoodDetailDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<GetFoodDetailBusinessLogic>();
                return busi.GetFoodDetail(inputDto);
            }
            catch (Exception)
            {
                return this.Output<FoodOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
