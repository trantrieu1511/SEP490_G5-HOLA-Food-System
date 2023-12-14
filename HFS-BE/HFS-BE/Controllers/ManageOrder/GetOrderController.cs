using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrder;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageOrder
{
    public class GetOrderController : BaseController
    {
        public GetOrderController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("orders/getOrdersSeller")]
        [Authorize]
        public OrderSellerDaoOutputDto GetOrderSeller(OrderSellerByStatusInputDto inputDto)
        {
            try
            {
                var business = this.GetBusinessLogic<GetOrderBusinessLogic>();
                Dao.OrderDao.OrderSellerByStatusInputDto inputBL = new Dao.OrderDao.OrderSellerByStatusInputDto
                {
                    Status = inputDto.Status,
                    UserId = GetUserInfor().UserId,
                    DateFrom = inputDto.DateFrom,
                    DateEnd = inputDto.DateEnd,
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
