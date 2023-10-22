using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Cart
{
    public class CheckOutCartController : BaseController
    {
        public CheckOutCartController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("checkout/createorder")]
        [Authorize(Roles = "3")]
        public BaseOutputDto CheckOutCart(CheckOutCartInputDto inputDto)
        {
            try
            {
                inputDto.CustomerId = this.GetUserInfor().UserId;
                var busi = this.GetBusinessLogic<CheckOutBusinessLogic>();
                return busi.CheckOutCart(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
