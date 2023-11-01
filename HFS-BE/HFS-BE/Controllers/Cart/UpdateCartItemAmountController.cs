using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Cart
{
    public class UpdateCartItemAmountController : BaseController
    {
        public UpdateCartItemAmountController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("cart/updateamount")]
        public BaseOutputDto UpdateAmount(UpdateAmoutCartItemDaoInputDto inputDto)
        {
            try
            {
                var userinfo = this.GetUserInfor();
                if (!userinfo.Role.Equals("CU"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }

                inputDto.CartId = userinfo.UserId;
                var busi = this.GetBusinessLogic<UpdateAmountCartItemBusinessLogic>();
                return busi.UpdateItemAmount(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
