using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrder;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageOrder
{
    public class AcceptOrderController : BaseController
    {
        public AcceptOrderController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("orders/acceptOrderSeller")]
        [Authorize]
        public BaseOutputDto AcceptOrder(OrderAcceptInputDto input)
        {
            try
            {
                var inputBL = mapper.Map<OrderAcceptInputDto, OrderProgressStatusInputDto>(input);
                inputBL.UserId = GetUserInfor().UserId;

                var cancelBL = GetBusinessLogic<AcceptOrderBusinessLogic>();
                return cancelBL.AcceptOrder(inputBL);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
