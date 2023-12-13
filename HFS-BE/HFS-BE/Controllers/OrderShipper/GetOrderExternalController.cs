using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.OrderShipper
{
    public class GetOrderExternalController : BaseController
    {
        public GetOrderExternalController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }


        [HttpGet("shipper/orderExternal")]
        public OrderExternalLstOutputDto GetAllOrder()
        {
            try
            {
                var busi = this.GetBusinessLogic<GetOrderExternalBusinessLogic>();
                return busi.GetAllOrderExternalShipper();
            }
            catch (Exception)
            {
                return this.Output<OrderExternalLstOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
