using AutoMapper;
using CloudinaryDotNet.Actions;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class DisplayOrderBusinessLogic : BaseBusinessLogic
    {
        public DisplayOrderBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public OrderSellerDaoOutputDto ListOrder(OrderSellerByStatusInputDto inputDto)
        {
            inputDto.UserId = "SE0000001";

            // get orders
            var orderDao = CreateDao<OrderDao>();
            var orders = orderDao.GetOrderByStatus(inputDto);
            //map dao output -> BL ouput
            var ordersMapper = mapper.Map<Dao.OrderDao.OrderSellerDaoOutputDto, OrderSellerDaoOutputDto>(orders);
            //convert image DB to Base64
            foreach(var order in orders.Orders)
            {
                var indexOrder = orders.Orders.IndexOf(order);
                // foreach list orderdetail have foodImage -> imageBase64
                foreach (var detail in order.OrderDetails)
                {
                    var indexDetail = order.OrderDetails.IndexOf(detail);
                    var imgFoodBase64 = ImageFileConvert.ConvertFileToBase64(detail.SellId, detail.Image, 1);

                    if (imgFoodBase64 == null)
                        continue;
                    var imgFoodMapper = mapper.Map<ImageFileConvert.ImageOutputDto, FoodImageOutputSellerDto>(imgFoodBase64);

                    // put back to element: orderdetail of ordersMapper list
                    ordersMapper.Orders[indexOrder].OrderDetails[indexDetail].ImageBase64 = imgFoodMapper;
                }
                //check status input != Completed = 4 -> orderProgress have no image 
                if (inputDto.Status != 4)
                    continue;
                //status input == 4 Completed : have image from shipper
                var orderProgressCompleted = order.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last();
                var indexProgressCompleted = order.OrderProgresses.IndexOf(orderProgressCompleted);

                var imgProgressBase64 = ImageFileConvert.ConvertFileToBase64(order.ShipperId, orderProgressCompleted.Image, 2);
                if (imgProgressBase64 == null)
                    continue;
                var imgProgressMapper = mapper.Map<ImageFileConvert.ImageOutputDto, ImageFoodOutputDto>(imgProgressBase64);

                // put back to element: orderProgress of ordersMapper list
                ordersMapper.Orders[indexOrder].OrderProgresses[indexProgressCompleted].ImageBase64 = imgProgressMapper;
            }

            return ordersMapper;
        }
    }
}
