using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Homepage;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Homepage
{
    public class SearchShopController : BaseController
    {
        public SearchShopController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        //[HttpPost("search")]
        //public IActionResult Create(SearchShopBusinessLogicInputDto inputDto)
        //{
        //    try
        //    {
        //        SearchShopBusinessLogic business = this.GetBusinessLogic<SearchShopBusinessLogic>();

        //        return Ok(business.SearchShop(inputDto));
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
