﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.OrderShipper
{
    //[Authorize(Roles = "4")]
    public class OrderOnShipperController : BaseController
    {
        public OrderOnShipperController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("shipper/order")]
        
        public OrderByShipperDaoOutputDto GetAllOrder(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                
                var busi = this.GetBusinessLogic<OrderShipperBusinessLogic>();
                var role = this.GetAccessRight();
                //if(role != 4)
                //{
                //    return this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdFail);
                //}
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
                return this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        [HttpPost("shipper/history")]
        //[Authorize(Roles = "4")]
        public OrderOnHistoryDaoOutputDto GetAllOrderHistory(OrderHistoryInputDto inputDto)
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

        [HttpPost("shipper/order/shipping")]
        //[Authorize(Roles = "4")]
        public BaseOutputDto ChangeStatus(OrderStatusInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<OrderShipperBusinessLogic>();
                

                return busi.ChangeStatus(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
