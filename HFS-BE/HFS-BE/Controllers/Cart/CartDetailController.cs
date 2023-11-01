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
        public CartDetailBusinessLogicOutputDto GetCartDetail(GetCartItemDaoInputDto inputDto)
        {
            try
            {
                var userInfor = this.GetUserInfor();
                inputDto.CartId = userInfor.UserId;
                var role = userInfor.Role;
                if (!role.Equals("CU"))
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
