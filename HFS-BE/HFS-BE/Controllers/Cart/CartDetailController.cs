using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HFS_BE.Controllers.Cart
{
    public class CartDetailController : BaseController
    {
        public CartDetailController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("cart/cartdetail")]
        [Authorize(Roles = "3")]
        public CartDetailBusinessLogicOutputDto GetCartDetail(GetCartItemDaoInputDto inputDto)
        {
            try
            {
                inputDto.CartId = this.GetUserInfor().UserId;
                var role = this.GetAccessRight();
                if (role != 3)
                {
                    return this.Output<CartDetailBusinessLogicOutputDto>(Constants.ResultCdSuccess, "You are not customer.");
                }

                var busi = this.GetBusinessLogic<CartDetailBusinessLogic>();
                return busi.GetCartDetail(inputDto);
            }
            catch (Exception)
            {
                return this.Output<CartDetailBusinessLogicOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
