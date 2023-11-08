using AutoMapper;
using CloudinaryDotNet.Actions;
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
                    output[indexOder].Status = OrderStatusEnum.GetOrderStatusString(status);
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
                
                order.ShipperId = inputDto.ShipperId;

                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, "Commmission Failed", "Add Internal Shipper Failed");
            }
        }

        public Order? GetOrderByOrderIdAndSellerId(int orderId, string sellerId)
        {
            return context.Orders.FirstOrDefault(o => o.OrderId == orderId && o.SellerId.Equals(sellerId));
        }

        public GetCustomerOrdersDaoOutputDto GetOrdersCustomer(GetOrdersCustomerDaoInputDto inputDto)
        {
            try
            {
                var query = context.Orders
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                        .ThenInclude(f => f.Category)
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                        .ThenInclude(f => f.FoodImages)
                    .Include(x => x.OrderProgresses)
                    .Include(x => x.Seller)
                    .Include(x => x.Voucher)
                    .Where(x => x.Status == inputDto.Status 
                                && x.OrderDate >= inputDto.DateFrom
                                && x.OrderDate <= inputDto.DateEnd);

                var result = query.Select(o => mapper.Map<Order, OrderCustomerDaoOutputDto>(o)).ToList();
                foreach (var item in result )
                {
                    foreach (var details in item.OrderDetails)
                    {
                        var feedbacks = this.context.Feedbacks
                            .Where(x => x.FoodId == details.FoodId
                                    && x.CustomerId == inputDto.CustomerId)
                            .ToList();

                        if (feedbacks.Any())
                        {
                            details.IsRated = true;
                        }
                        else details.IsRated = false;

                    }
                }

                var output = this.Output<GetCustomerOrdersDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListOrders = result;
                return output;
            }
            catch (Exception)
            {
                return this.Output<GetCustomerOrdersDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto CancelOrderCustomer(OrderCustomerDaoInputDto inputDto)
        {
            try
            {
                var order = this.context.Orders
                    .FirstOrDefault(x => x.OrderId == inputDto.OrderId
                                        && x.CustomerId.Equals(inputDto.CustomerId));

                order.Status = 6;
                this.context.Update(order);
                this.context.SaveChanges();
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public OrderCustomerDaoOutputDto GetOrderCustomer(OrderCustomerDaoInputDto inputDto)
        {
            try
            {
                var order = this.context.Orders
                    .FirstOrDefault(x => x.OrderId == inputDto.OrderId
                                        && x.CustomerId.Equals(inputDto.CustomerId));

                var output = this.Output<OrderCustomerDaoOutputDto>(Constants.ResultCdSuccess);
                if (order != null)
                {
                    output = this.mapper.Map<Order, OrderCustomerDaoOutputDto>(order);
                }
                else return null;
                return output;
            }
            catch (Exception)
            {
                return this.Output<OrderCustomerDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public GetCustomerOrdersDaoOutputDto GetOrderCustomerFoodId(GetOrdersCustomerFoodIdDaoInputDto inputDto)
        {
            try
            {
                var query = context.Orders
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                        .ThenInclude(f => f.Category)
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                        .ThenInclude(f => f.FoodImages)
                    .Include(x => x.OrderProgresses)
                    .Include(x => x.Seller)
                    .Include(x => x.Voucher)
                    .Where(x => x.CustomerId.Equals(inputDto.CustomerId) 
                                && x.OrderDetails.Where(x => x.FoodId == inputDto.FoodId).Any())
                    .OrderByDescending(x => x.OrderId);

                var result = query.Select(o => mapper.Map<Order, OrderCustomerDaoOutputDto>(o)).ToList();

                var output = this.Output<GetCustomerOrdersDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListOrders = result;
                return output;
            }
            catch (Exception)
            {
                return this.Output<GetCustomerOrdersDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public OrderExternalLstOutputDto GetAllOrderExternalShipper()
        {
            try
            {
                var query = context.Orders
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                        .ThenInclude(f => f.Category)
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Food)
                        .ThenInclude(f => f.FoodImages)
                    .Include(x => x.Customer)
                    .Include(x => x.Voucher)
                    .Include(x => x.OrderProgresses)
                    .Include(x => x.Seller)
                    .Where(x => x.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Status == 2 //check order is Wait_Shipper = 2,
                        && x.ShipperId == null // null because orderprogress : wait shipper but shipperId in order null -> wait external shipper
                );

                var result = query.Select(o => mapper.Map<Order, OrderExternalShipperOutputDto>(o)).ToList();
                var output = Output<OrderExternalLstOutputDto>(Constants.ResultCdSuccess);
                output.Orders = result;
                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
