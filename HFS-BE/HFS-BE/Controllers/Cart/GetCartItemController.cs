using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Cart
{
    public class GetCartItemController : BaseController
    {
        public GetCartItemController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost]
        [Route("getcartitem")]
        [Authorize(Roles = "3")]
        public GetCartItemDaoOutputDto GetCartItem(GetCartItemDaoInputDto inputDto)
        {
            try
            {
                inputDto.CartId = this.GetUserInfor().UserId;
                var role = this.GetAccessRight();
                if (role != 3)
                {
                    return this.Output<GetCartItemDaoOutputDto>(Constants.ResultCdSuccess, "You are not customer.");
                }

                var busi = this.GetBusinessLogic<GetCartItemBusinessLogic>();
                return busi.GetCartItem(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetCartItemDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
