using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Controllers.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.AdminDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderProgressBusinessLogic : BaseBusinessLogic
    {
        public OrderProgressBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto CreateOrderProgress(OrderProgressBusinessLogicInputDto inputDto, out string? customerId, out List<Models.Admin> admins, out string? sellerId)
        {
            customerId = null;
            admins = null;
            sellerId = null;
            try
            {
                var orderDao = CreateDao<OrderDao>();
                var notifyDao = CreateDao<NotificationDao>();
                var adminDao = CreateDao<AdminDao>();

                var order = orderDao.GetOrderByOrderId(inputDto.OrderId);
                //check order
                if (order == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Order is not exist");
                // put customerId
                customerId = order?.CustomerId;
                sellerId = order?.SellerId;

                string fileNames = null;
                if (inputDto.Image != null)
                {
                    fileNames = ReadSaveImage.SaveImagesOrderProgress(inputDto.Image, inputDto.UserDto, 2);
                   
                } 
                var dao = this.CreateDao<OrderProgressDao>();
                var inputMapper = mapper.Map<OrderProgressBusinessLogicInputDto, DAO.OrderProgressDao.OrderProgressDaoInputDto>(inputDto);
                inputMapper.Image = fileNames;
                inputMapper.ShipperId = inputDto.UserDto.UserId;
                BaseOutputDto baseOutputDto = dao.CreateOrderProgress(inputMapper);
                if (!baseOutputDto.Success)
                    return baseOutputDto;

                var notifyCus = new List<NotificationAddNewInputDto>();
                var notifySell = new List<NotificationAddNewInputDto>();
                //shipping
                if (inputDto.Status == 3)
                {
                    //gen title and content notification
                    notifyCus = GenerateNotification.GetSingleton().GenNotificationOrderShipping(customerId, order.OrderId);
                    notifySell = GenerateNotification.GetSingleton().GenNotificationOrderShipping(order.SellerId, order.OrderId);
                }
                //completed
                else if (inputDto.Status == 4)
                {
                    notifyCus = GenerateNotification.GetSingleton().GenNotificationOrderShippedSuccess(customerId, order.OrderId);
                    notifySell = GenerateNotification.GetSingleton().GenNotificationOrderShippedSuccess(order.SellerId, order.OrderId);
                }
                // incompleted
                else
                {
                    notifyCus = GenerateNotification.GetSingleton().GenNotificationOrderShippedFail(customerId, order.OrderId);
                    notifySell = GenerateNotification.GetSingleton().GenNotificationOrderShippedFail(order.SellerId, order.OrderId);
                }

                //3. add notify
                var notiCus = notifyDao.AddNewNotification(notifyCus);
                var notiSel = notifyDao.AddNewNotification(notifySell);
                if (!notiCus.Success || !notiSel.Success)
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                // send to admin
                if (inputDto.Status == 5)
                {
                    var adminLst = adminDao.GetAdmins();
                    admins = adminLst;
                    foreach(var ad in adminLst)
                    {
                        var notify = GenerateNotification.GetSingleton().GenNotificationOrderShippedFail(ad.AdminId, order.OrderId);
                        var outputNoti = notifyDao.AddNewNotification(notify);
                        if (!outputNoti.Success)
                            return Output<BaseOutputDto>(Constants.ResultCdFail);
                    }
                }

                return Output<BaseOutputDto>(Constants.ResultCdSuccess); ;
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
