using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HFS_BE.Controllers.Cart
{
    public class AddCartItemController : BaseController
    {
        public AddCartItemController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("cart/addtocart")]
        public BaseOutputDto AddCartItem(AddCartItemInputDto inputDto)
        {
            try
            {
                var role = this.GetAccessRight();
                var busi = this.GetBusinessLogic<AddCartItemBusinessLogic>();
                if (role != 3)
                {
                    return this.Output<GetCartItemDaoOutputDto>(Constants.ResultCdSuccess, "You are not customer.");
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
