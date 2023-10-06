using HFS_BE.Business.ViewShop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ViewShop
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchShopController : ControllerBase
    {

        [HttpPost("search")]
        public IActionResult Create(SearchShopInputDto inputDto)
        {
            try
            {
                SearchShopBusiness business = new SearchShopBusiness();
                return Ok(business.SearchShop(inputDto));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
