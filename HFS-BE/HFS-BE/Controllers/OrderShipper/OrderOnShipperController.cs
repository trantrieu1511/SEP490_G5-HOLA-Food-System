using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Controllers.OrderShipper
{
    //[Authorize(Roles = "4")]
    public class OrderOnShipperController : BaseControllerSignalR
    {
        public OrderOnShipperController(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory) : base(context, mapper, hubContextFactory)
        {
        }

        [HttpPost("shipper/order")]
        public OrderByShipperBLOutputDto GetAllOrder(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderShipperBusinessLogic>();                
                return busi.ListOrderShipper(inputDto);   
            }
            catch (Exception)
            {
                return this.Output<OrderByShipperBLOutputDto>(Constants.ResultCdFail);
            }
        }

        [HttpPost("shipper/orderprogress")]
        //[Authorize(Roles = "SH")]
        public async Task<BaseOutputDto> CreateOrderProgress([FromForm] OrderProgressControllerInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderProgressBusinessLogic>();
                //    var role = this.GetAccessRight();
                var inputBL = mapper.Map<Controllers.OrderShipper.OrderProgressControllerInputDto,
                    BusinessLogic.OrderShipper.OrderProgressBusinessLogicInputDto>(inputDto);
                inputBL.UserDto = this.GetUserInfor();
                string? customerId = "";
                string? sellerId = "";
                List<Models.Admin> admins = new List<Models.Admin>();
                var output = busi.CreateOrderProgress(inputBL, out customerId, out admins, out sellerId);
                if (output.Success )
                {
                    //notify for customer
                    var notifyHub = _hubContextFactory.CreateHub<NotificationHub>();
                    await notifyHub.Clients.Group(customerId).SendAsync("notification");
                    await notifyHub.Clients.Group(sellerId).SendAsync("notification");

                    if (inputDto.Status == 5 && admins is not null && admins.Count > 0)
                    {
                        foreach (var ad in admins)
                        {
                            await notifyHub.Clients.Group(ad.AdminId).SendAsync("notification");
                        }
                    }
                }
                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        [HttpPost("shipper/history/detail")]
        //[Authorize(Roles = "4")]
        public BaseOutputDto GetHistoryDetail(int orderId)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderShipperBusinessLogic>();
                //    var role = this.GetAccessRight();

                return busi.ListOrder(orderId);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        [HttpPost("shipper/history")]
        //[Authorize(Roles = "4")]
        public OrderByShipperBLOutputDto GetAllOrderHistory(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderHistoryBusinessLogic>();
                var role = this.GetAccessRight();
                
                return busi.ListOrderHistory(inputDto);
            }
            catch (Exception)
            {
                return this.Output<OrderByShipperBLOutputDto>(Constants.ResultCdFail);
            }
        }       
    }
}
