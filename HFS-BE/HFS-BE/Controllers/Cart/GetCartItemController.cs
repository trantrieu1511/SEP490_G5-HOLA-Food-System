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
        public GetCartItemController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost]
        [Route("cart/getcartitem")]
        public GetCartItemDaoOutputDto GetCartItem()
        {
            try
            {
                var userInfor = this.GetUserInfor();
                var inputDto = new GetCartItemDaoInputDto();
                inputDto.CartId = userInfor.UserId;
                if (!userInfor.Role.Equals("CU"))
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
