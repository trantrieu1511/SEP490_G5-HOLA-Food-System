using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrder;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageOrder
{
    public class CancelOrderController : BaseController
    {
        public CancelOrderController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("orders/cancelOrderSeller")]
        [Authorize]
        public BaseOutputDto CancelOrder(OrderCancelInputDto input)
        {
            try
            {
                var inputBL = mapper.Map<OrderCancelInputDto, OrderProgressCancelInputDto>(input);
                inputBL.UserId = GetUserInfor().UserId;

                var cancelBL = GetBusinessLogic<CancelOrderBusinessLogic>();
                return cancelBL.CancelOrder(inputBL);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
