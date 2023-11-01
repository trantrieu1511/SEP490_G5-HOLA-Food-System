using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrder;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageOrder
{
    public class CommissionInternalShipperController : BaseController
    {
        public CommissionInternalShipperController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("orders/internalShipperOrderSeller")]
        public BaseOutputDto AcceptOrder(OrderInternalInputDto input)
        {
            try
            {
                var inputBL = mapper.Map<OrderInternalInputDto, OrderInternalShipInputDto>(input);
                inputBL.UserId = GetUserInfor().UserId;

                var internalBL = GetBusinessLogic<CommissionInternalShipperBL>();
                return internalBL.CommissionInternal(inputBL);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
