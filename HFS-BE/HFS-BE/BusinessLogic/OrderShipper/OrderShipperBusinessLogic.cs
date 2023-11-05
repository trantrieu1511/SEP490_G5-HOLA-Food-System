using AutoMapper;
using CloudinaryDotNet.Actions;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderShipperBusinessLogic : BaseBusinessLogic
    {
        public OrderShipperBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public OrderByShipperBLOutputDto ListOrderShipper(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<OrderDao>();

                var output = dao.OrderOfShipper(inputDto);
                var ouputMapper = mapper.Map<Dao.OrderDao.OrderByShipperDaoOutputDto, OrderByShipperBLOutputDto>(output);
                
                foreach (var order in output.Orders)
                {
                    var indexOrder = output.Orders.IndexOf(order);
                    foreach (var detail in order.OrderDetails)
                    {
                        var imageInfor = ImageFileConvert.ConvertFileToBase64(detail.SellerId, detail.Image, 1);
                        if (imageInfor == null)
                            continue;
                        var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, ImageFoodOutputDto>(imageInfor);
                        var indexDetail = order.OrderDetails.IndexOf(detail);
                        ouputMapper.Orders[indexOrder].OrderDetails[indexDetail].ImageBase64 = imageMapper;

                    }

                    //lay last orderprogress
                    var lastOP = order.OrderProgress.OrderBy(x => x.CreateDate).AsQueryable().Last();
                    var indexProgressCompleted = order.OrderProgress.IndexOf(lastOP);
                    if (lastOP.Status != 4)
                        continue;
                    //lastOP.Image -> base64
                    var imgProgressBase64 = ImageFileConvert.ConvertFileToBase64(order.Status, lastOP.Image, 4);
                    if (imgProgressBase64 == null)
                        continue;
                    var imgProgressMapper = mapper.Map<ImageFileConvert.ImageOutputDto, ImageFoodOutputDto>(imgProgressBase64);
                    ouputMapper.Orders[indexOrder].OrderProgress[indexProgressCompleted].ImageBase64 = imgProgressMapper;
                }

                return ouputMapper;
            }
            catch (Exception)
            {

                return this.Output<OrderByShipperBLOutputDto>(Constants.ResultCdFail);
            }
        }
        public OrderHistoryDetailDtoOutput ListOrder(int orderId)
        {
            try
            {
                var dao = this.CreateDao<OrderDao>();
                return dao.OrderHistoryDetail(orderId);
            }
            catch (Exception)
            {

                return this.Output<OrderHistoryDetailDtoOutput>(Constants.ResultCdFail);
            }
        }

       
    }
}
