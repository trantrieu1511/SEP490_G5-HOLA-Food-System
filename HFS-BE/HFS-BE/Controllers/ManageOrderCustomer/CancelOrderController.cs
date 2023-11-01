using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrderCustomer;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageOrderCustomer
{
    public class CancelOrderController : BaseController
    {
        public CancelOrderController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("order/cancel")]
        [Authorize]
        public BaseOutputDto CancelOrder(OrderCustomerDaoInputDto inputDto)
        {
            try
            {
                var userInfo = this.GetUserInfor();
                if (!userInfo.Role.Equals("CU"))
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You are not customer!");
                }
                inputDto.CustomerId = userInfo.UserId;
                var busi = this.GetBusinessLogic<CancelOrderBusinessLogic>();
                return busi.CancelOrder(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
