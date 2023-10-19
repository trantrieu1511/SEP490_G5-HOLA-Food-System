using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Homepage;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Homepage
{
	public class DisplayShopController : BaseController
    {
        public DisplayShopController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {

        }

        //[Authorize]
        [HttpPost("home/displayshop")]
		
		public DisplayShopOutputDto Create()
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayShopBusinessLogic>();
                var user = this.GetAccessRight();
                var output = business.DisplayShop();
                return output;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
