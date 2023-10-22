using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Cart
{
    public class DeleteCartItemController : BaseController
    {
        public DeleteCartItemController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("cart/deleteitem")]
        public BaseOutputDto DeleteCartItem(DeleteCartItemInputDto inputDto)
        {
            try
            {
                var userinfo = this.GetUserInfor();
                if (userinfo.RoleId != 3)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }

                inputDto.CartId = userinfo.UserId;
                var busi = this.GetBusinessLogic<DeleteCartItemBusinessLogic>();                
                return busi.DeleteCartItem(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
