using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderHistoryBusinessLogic : BaseBusinessLogic
    {
        public OrderHistoryBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public OrderByShipperBLOutputDto ListOrderHistory(OrderByShipperDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<OrderDao>();

                var output = dao.OrderHistory(inputDto);

                var ouputMapper = new OrderByShipperBLOutputDto();
                if (output.Orders.Count > 0)
                {
                    ouputMapper = mapper.Map<Dao.OrderDao.OrderHistoryDaoOutputDto, OrderByShipperBLOutputDto>(output);
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
                    }
                }

                return ouputMapper;
            }
            catch (Exception)
            {

                return this.Output<OrderByShipperBLOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
