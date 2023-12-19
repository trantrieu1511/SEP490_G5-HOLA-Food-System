using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Search;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Search
{
    public class SearchController : BaseController
    {
        public SearchController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("home/search")]
        public IActionResult Search(FoodByNameDaoInputDto inputDto)
        {
            try
                {
                var busi = this.GetBusinessLogic<SearchBusinessLogic>();
                var busi2 = this.GetBusinessLogic<SearchShopBusinessLogic>();
                if (inputDto.Type.Equals("0"))
                {
                    return Ok(busi.Search(inputDto));
                }
                return Ok(busi2.SearchShop(new SearchShopInputDto()
                {
                    Key = inputDto.SearchKey,
                    pageNum = inputDto.pageNum,
                    pageSize = inputDto.pageSize,
                }));
            }
            catch (Exception)
            {
                return Ok(this.Output<FoodShopDaoOutputDto>(Constants.ResultCdFail));
            }
        }
    }
}
