using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrder;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageOrder
{
    public class DisplayOrderController : BaseController
    {
        public DisplayOrderController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("orders/getOrdersSeller")]
        public OrderSellerDaoOutputDto DisplayOrderSeller(OrderSellerByStatusInputDto inputDto)
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayOrderBusinessLogic>();
                Dao.OrderDao.OrderSellerByStatusInputDto inputBL = new Dao.OrderDao.OrderSellerByStatusInputDto
                {
                    Status = inputDto.Status,
                    UserId = GetUserInfor().UserId
                };
                return business.ListOrder(inputBL);
            }
            catch (Exception)
            {
                return this.Output<OrderSellerDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
