using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.OrderShipper
{
  
    public class OrderOnShipperController : BaseController
    {
        public OrderOnShipperController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("shipper/order")]
        [Authorize(Roles ="4")]
        public OrderByShipperDaoOutputDto GetAll(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderShipperBusinessLogic>();
                var role = this.GetAccessRight();
                if(role != "4")
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
        [HttpPost("shipper/orderprogress")]
        //[Authorize(Roles = "4")]
        public BaseOutputDto Get(OrderProgressDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderProgressBusinessLogic>();
            //    var role = this.GetAccessRight();
               
                return busi.CreateOrderProgress(inputDto);
            }
            catch (Exception)
            {
                return this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        [HttpPost("shipper/history/detail")]
        //[Authorize(Roles = "4")]
        public BaseOutputDto GetHistory(int orderId)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderShipperBusinessLogic>();
                //    var role = this.GetAccessRight();

                return busi.ListOrder(orderId);
            }
            catch (Exception)
            {
                return this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        [HttpPost("shipper/history")]
        [Authorize(Roles = "4")]
        public OrderOnHistoryDaoOutputDto GetAll(OrderHistoryInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderHistoryBusinessLogic>();
                var role = this.GetAccessRight();
                
                return busi.ListOrder(inputDto);
            }
            catch (Exception)
            {
                return this.Output<OrderOnHistoryDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
