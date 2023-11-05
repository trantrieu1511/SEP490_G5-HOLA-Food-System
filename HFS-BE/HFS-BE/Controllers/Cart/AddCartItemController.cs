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
    public class AddCartItemController : BaseController
    {
        public AddCartItemController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("cart/addtocart")]
        [Authorize]
        public BaseOutputDto AddCartItem(AddCartItemInputDto inputDto)
        {
            try
            {
                var userInfor = this.GetUserInfor();
                inputDto.CartId = userInfor.UserId;
                var busi = this.GetBusinessLogic<AddCartItemBusinessLogic>();
                if (!userInfor.Role.Equals("CU"))
                {
                    return this.Output<GetCartItemDaoOutputDto>(Constants.ResultCdFail, "You are not customer.");
                }

                return busi.AddCartItem(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
