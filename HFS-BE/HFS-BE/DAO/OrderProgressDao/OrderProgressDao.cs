using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.OrderProgressDao
{
    public class OrderProgressDao : BaseDao
    {
        public OrderProgressDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto CreateOrderProgress(OrderProgressDaoInputDto inputDto)
        {
            try
            {
                //get orderprogress by inputDto.orderId
                var data = this.context.OrderProgresses.Where(x=>x.OrderId == inputDto.OrderId).ToList();
                if(data.FirstOrDefault(x=>x.Status == inputDto.Status) != null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail);
                }
                
                // check trong list trả về có status = inputDto.status ko
                // có -> return ....
                // ko làm tiếp bên dưới

                //var data = context.Orders.FirstOrDefault(x => x.OrderId == inputDto.OrderId);

                inputDto.CreateDate = DateTime.Now; 
                var order = mapper.Map<OrderProgressDaoInputDto, OrderProgress>(inputDto);
                order.ShipperId = inputDto.ShipperId;
                context.OrderProgresses.Add(order);
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto CreateOrderProgressCustomer(OrderCreateDaoInputDto inputDto)
        {
            try
            {
                var order = mapper.Map<OrderCreateDaoInputDto, OrderProgress>(inputDto);
                context.OrderProgresses.Add(order);
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto AddOrderProgressCommonStatus(OrderProgressStatusInputDto inputDto)
        {
            try
            {
                //var data = context.Orders.FirstOrDefault(x => x.OrderId == inputDto.OrderId);

                OrderProgress orderProgress = new OrderProgress
                {
                    CreateDate = DateTime.Now,
                    SellerId = inputDto.UserId,
                    OrderId = inputDto.OrderId,
                    Status = inputDto.Status
                };

                context.OrderProgresses.Add(orderProgress);
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto AddOrderProgressCancelStatus(OrderProgressCancelInputDto inputDto)
        {
            try
            { 
                OrderProgress orderProgress = new OrderProgress
                {
                    CreateDate = DateTime.Now,
                    SellerId = inputDto.UserId,
                    OrderId = inputDto.OrderId,
                    Status = inputDto.Status,
                    Note = inputDto.Note
                };

                context.OrderProgresses.Add(orderProgress);
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public List<OrderProgress> GetOrderProgresByOrderId(int orderId)
        {
            return context.OrderProgresses.Where(x => x.OrderId == orderId).ToList();
        }

        public BaseOutputDto AddNewOrderProgress(OrderProgress orderProgress)
        {
            try
            {
                context.OrderProgresses.Add(orderProgress);
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

    }
}
