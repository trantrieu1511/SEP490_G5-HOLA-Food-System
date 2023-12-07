using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Cart
{
    public class GetCusAddressController : BaseController
    {
        public GetCusAddressController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("checkout/address")]
        [Authorize]
        public GetUserAddressDaoOutputDto GetAddress()
        {
            try
            {
                var userInfor = this.GetUserInfor();
                var busi = this.GetBusinessLogic<GetCusAddressBusinessLogic>();
                var input = new GetAddressInfoInputDto()
                {
                    UserId = userInfor.UserId,
                };

                return busi.GetAddresss(input);
            }
            catch (Exception)
            {
                return this.Output<GetUserAddressDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
