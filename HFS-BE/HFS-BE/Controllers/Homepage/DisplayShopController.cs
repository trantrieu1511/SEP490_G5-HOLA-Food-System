using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Homepage;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Homepage
{
    public class DisplayShopController : BaseController
    {
        public DisplayShopController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPost("home/displayshop")]
        public DisplayShopOutputDto Create(DisplayShopInputDto inputDto)
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayShopBusinessLogic>();

                return business.DisplayShop(inputDto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
