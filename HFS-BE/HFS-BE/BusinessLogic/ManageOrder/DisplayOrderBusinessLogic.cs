using AutoMapper;
using CloudinaryDotNet.Actions;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using HFS_BE.Utils.IOFile;
using System.Text;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class DisplayOrderBusinessLogic : BaseBusinessLogic
    {
        public DisplayOrderBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public OrderSellerDaoOutputDto ListOrder(OrderSellerByStatusInputDto inputDto)
        {
            /*inputDto.UserId = "SE00000001";*/
            // get orders
            var orderDao = CreateDao<OrderDao>();
            var orders = orderDao.GetOrderByStatus(inputDto);

            if (!orders.Success)
                return Output<OrderSellerDaoOutputDto>(Constants.ResultCdFail);

            if(orders.Orders is null)
                return Output<OrderSellerDaoOutputDto>(Constants.ResultCdSuccess);

            if (orders.Orders.Count < 1)
                return Output<OrderSellerDaoOutputDto>(Constants.ResultCdSuccess);

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
                //check status input != Completed = 4, InCompleted = 5 -> orderProgress have no image 
                if (inputDto.Status != 4 && inputDto.Status != 5)
                    continue;

                //status input == 4 || 5 last: have image from shipper
                var orderProgressInOrCompleted = order.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last();
                var indexProgressInOrCompleted = order.OrderProgresses.IndexOf(orderProgressInOrCompleted);

                if(inputDto.Status == 5)
                {
                    ordersMapper.Orders[indexOrder].OrderProgresses[indexProgressInOrCompleted].Note = GetNote(orderProgressInOrCompleted.Note);
                }

                var imgProgressBase64 = ImageFileConvert.ConvertFileToBase64(order.ShipperId, orderProgressInOrCompleted.Image, 2);
                if (imgProgressBase64 == null)
                    continue;
                var imgProgressMapper = mapper.Map<ImageFileConvert.ImageOutputDto, ImageFoodOutputDto>(imgProgressBase64);

                // put back to element: orderProgress of ordersMapper list
                ordersMapper.Orders[indexOrder].OrderProgresses[indexProgressInOrCompleted].ImageBase64 = imgProgressMapper;
            }

            return ordersMapper;


            string GetNote(string note)
            {
                string[] notes = note.Split(",");
                string result = "";
                foreach(var n in notes)
                {
                    result += InCompleteEnum.GetStatusString(n) + ", ";
                }

                if (!string.IsNullOrEmpty(result))
                {
                    result = result.TrimEnd(',', ' ');
                }

                return result;
            }
        }
    }
}
