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
    public class GetCustomerOrdersController : BaseController
    {
        public GetCustomerOrdersController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("order/history")]
        [Authorize]
        public GetCustomerOrdersDaoOutputDto GetCustomerOrders(GetOrdersCustomerDaoInputDto inputDto)
        {
            try
            {
                var userInfo = this.GetUserInfor();
                if (userInfo != null && !userInfo.Role.Equals("CU"))
                {
                    return this.Output<GetCustomerOrdersDaoOutputDto>(Constants.ResultCdSuccess, "You are not customer!");
                }
                inputDto.CustomerId = userInfo.UserId;
                var busi = this.GetBusinessLogic<GetCustomerOrdersBusinessLogic>();
                return busi.GetCustomerOrders(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetCustomerOrdersDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
