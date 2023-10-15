using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.OrderShipper
{
    [Authorize]
    public class OrderOnShipperController : BaseController
    {
        public OrderOnShipperController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("shipper/order")]
        public OrderByShipperDaoOutputDto GetAll(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderShipperBusinessLogic>();
                var role = this.GetAccessRight();
                if(role != 4)
                {
                    return this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdFail);
                }
                return busi.ListOrderShipper(inputDto);
            }
            catch (Exception)
            {
                return this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdFail);
            }
        }
        
    }
}
