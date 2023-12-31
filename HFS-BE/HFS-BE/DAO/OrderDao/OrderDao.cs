﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.Dao.OrderDao
{
    public class OrderDao : BaseDao
    {
        public OrderDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public OrderByShipperDaoOutputDto OrderOfShipper(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                var status = inputDto.Status ? 2 : 3;
                // where lấy bên progress 
                var data = this.context.Orders
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food).ThenInclude(f => f.FoodImages)
                    .Include(x => x.OrderProgresses)
                    .Where(x => x.ShipperId == inputDto.ShipperId && x.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Status == status)
                    //.Select(x => mapper.Map<Order, OrderDaoOutputDto>(x))
                    .ToList();
                var output = mapper.Map<List<Order>, List<OrderDaoOutputDto>>(data);
                foreach (var item in data ) 
                { 
                    var indexOder = data.IndexOf(item);
                    foreach(var detail in item.OrderDetails)
                    {
                        var indexDetail = item.OrderDetails.ToList().IndexOf(detail);
                        var image = detail.Food.FoodImages.AsQueryable().First().Path;
                        output[indexOder].OrderDetails[indexDetail].Image = image;
                        output[indexOder].OrderDetails[indexDetail].FoodName = detail.Food.Name;
                        output[indexOder].OrderDetails[indexDetail].SellerId = detail.Food.SellerId;
                    }
                }
                var output1 = this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdSuccess);
                output1.Orders = output;
                return output1;

            }
            catch (Exception)
            {

                return this.Output<OrderByShipperDaoOutputDto>(Constants.ResultCdFail);
            }
        }
        public OrderHistoryDetailDtoOutput OrderHistoryDetail(int orderId)
        {
            try
            {
                var data = this.context.Orders.
                    Include(x => x.OrderProgresses).
                    Include(x => x.OrderDetails).ThenInclude(x => x.Food)

                    .Where(x => x.OrderId == orderId)
                    .Select(x => mapper.Map<Order, OrderHistoryDetailDtoOutput>(x)).FirstOrDefault();
                var output = this.Output<OrderHistoryDetailDtoOutput>(Constants.ResultCdSuccess);
                output = data;
                return output;
            }
            catch (Exception)
            {

                return this.Output<OrderHistoryDetailDtoOutput>(Constants.ResultCdFail);
            }
        }

        public OrderHistoryDaoOutputDto OrderHistory(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                // where lấy bên progress 
                var data = this.context.Orders
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food).ThenInclude(f => f.FoodImages)
                    .Include(x => x.OrderProgresses)
                    .Where(x => x.ShipperId == inputDto.ShipperId
                        && (x.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Status == 4
                            || x.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Status == 5)
                        )
                    //.Select(x => mapper.Map<Order, OrderDaoOutputDto>(x))
                    .ToList();
                var output = mapper.Map<List<Order>, List<OrderDaoOutputDto>>(data);
                foreach (var item in data)
                {
                    var indexOder = data.IndexOf(item);
                    foreach (var detail in item.OrderDetails)
                    {
                        var indexDetail = item.OrderDetails.ToList().IndexOf(detail);
                        var image = detail.Food.FoodImages.AsQueryable().First().Path;
                        output[indexOder].OrderDetails[indexDetail].Image = image;
                        output[indexOder].OrderDetails[indexDetail].FoodName = detail.Food.Name;
                        output[indexOder].OrderDetails[indexDetail].SellerId = detail.Food.SellerId;
                    }
                    var status = item.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Status;
                    output[indexOder].Status = OrderStatus.GetOrderStatusString(status);
                }
                var output1 = this.Output<OrderHistoryDaoOutputDto>(Constants.ResultCdSuccess);
                output1.Orders = output;
                return output1;

            }
            catch (Exception)
            {

                return this.Output<OrderHistoryDaoOutputDto>(Constants.ResultCdFail);
            }
        }
        public CheckOutOrderDaoOutputDto CheckOutOrder(CheckOutOrderDaoInputDto inputDto)
        {
            try
            {
                var order = mapper.Map<CheckOutOrderDaoInputDto, Order>(inputDto);
                foreach (var item in order.OrderDetails)
                {
                    item.UnitPrice = this.context.Foods.Where(x => x.FoodId == item.FoodId).FirstOrDefault().UnitPrice;
                }

                context.Add(order);
                context.SaveChanges();

                var output = this.Output<CheckOutOrderDaoOutputDto>(Constants.ResultCdSuccess);
                output.OrderId = order.OrderId;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<CheckOutOrderDaoOutputDto>(Constants.ResultCdFail, e.Message);
            }
        }
    
        public OrderSellerDaoOutputDto GetOrderByStatus(OrderSellerByStatusInputDto inputDto)
        {
            try
            {
                var query = context.Orders
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                        .ThenInclude(f => f.Category)
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                        .ThenInclude(f => f.FoodImages)
                    .Include(x => x.OrderProgresses)
                    .Include(x => x.Customer)
                    .Include(x => x.Voucher);

                if (inputDto.Status == 2 || inputDto.Status == 3 || inputDto.Status == 4 || inputDto.Status == 5)
                {
                    query = query.Include(x => x.Shipper).Include(x => x.Voucher);
                }

                var data = new List<Order>();
                List<int> arrayStatus = new List<int>
                    {
                        0, 1, 2, 3
                    };
                // check dateFrom == dateTo
                // or stasus: requested, prapring, wait shipper, shipping
                // -> not where date
                if (inputDto.DateFrom.Equals(inputDto.DateEnd) || arrayStatus.Contains((int)inputDto.Status))
                {
                    data = query.Where(x => x.SellerId.Equals(inputDto.UserId)
                            && x.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Status == inputDto.Status)
                            .ToList();
                }
                else
                {
                    data = query.Where(x => x.SellerId.Equals(inputDto.UserId)
                            && x.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Status == inputDto.Status
                            && x.OrderDate >= inputDto.DateFrom && x.OrderDate <= inputDto.DateEnd)
                            .ToList();
                }

                

                /*var result = data.Select(o => new OrderDaoSellerOutputDto
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer != null ? o.Customer.FirstName + " " + o.Customer.LastName : null ,
                    OrderDate = o.OrderDate != null ? o.OrderDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null,
                    RequiredDate = o.RequiredDate != null ? o.RequiredDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null,
                    ShippedDate = o.ShippedDate != null ? o.ShippedDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null,
                    ShipAddress = o.ShipAddress,
                    ShipperId = o.ShipperId,
                    ShipperName = o.Shipper != null ? o.Shipper.FirstName + " " + o.Shipper.LastName : null,
                    VoucherId = o.VoucherId,
                    TotalPrice = o.OrderDetails.Select(d => d.UnitPrice * d.Quantity).ToList().Sum(), //* them voucher,
                    Detail = new DetailProgress
                    {
                        Image = o.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Image,
                        Note = o.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Note,
                        CreateDate = o.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().CreateDate.Value.ToString("MM/dd/yyyy - HH:mm:ss")
                    },
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailFoodDto
                    {
                        OrderId = od.OrderId,
                        FoodId = od.FoodId,
                        FoodName = od.Food.Name,
                        UnitPrice = od.UnitPrice,
                        Quantity = od.Quantity,
                        Image = od.Food.FoodImages.ToList().First().Path,
                        CategoryName = od.Food.Category.Name
                    }).ToList()
                }).ToList();*/

                var result = data.Select(o => mapper.Map<Order, OrderDaoSellerOutputDto>(o)).ToList();


                
                var output = this.Output<OrderSellerDaoOutputDto>(Constants.ResultCdSuccess);
                output.Orders = result;
                return output;

            }
            catch (Exception)
            {

                return this.Output<OrderSellerDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto AddOrderInternalShipper(OrderInternalShipInputDto inputDto)
        {
            try
            {
                var order = context.Orders.First(o => o.OrderId.Equals(inputDto.OrderId));
                // vut qua ben BL
                // check shipperId null ko
                /*if (order == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order is not exist");*/

                order.ShipperId = inputDto.ShipperId;

                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Add Internal Shipper Failed");
            }
        }

        public Order? GetOrderByOrderId(string orderId)
        {
            return context.Orders.FirstOrDefault(o => o.OrderId.Equals(orderId));
        }
    }
}
