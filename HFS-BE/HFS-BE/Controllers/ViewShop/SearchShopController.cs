using HFS_BE.Base;
using HFS_BE.Business.ViewShop;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ViewShop
{
    [ApiController]
    public class SearchShopController : BaseController
    {
        public SearchShopController(SEP490_HFSContext context) : base(context)
        {
        }

        [HttpPost("search")]
        public IActionResult Create(SearchShopInputControllerDto inputDto)
        {
            try
            {
                SearchShopBusiness business = this.GetBusiness<SearchShopBusiness>();
                SearchShopInputDto input = new SearchShopInputDto()
                {
                    Name = inputDto.Name,
                };

                return Ok(business.SearchShop(input));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
