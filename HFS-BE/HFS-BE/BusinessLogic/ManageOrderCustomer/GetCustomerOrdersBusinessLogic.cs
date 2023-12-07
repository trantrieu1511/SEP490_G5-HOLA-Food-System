using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageOrderCustomer
{
    public class GetCustomerOrdersBusinessLogic : BaseBusinessLogic
    {
        public GetCustomerOrdersBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetCustomerOrdersDaoOutputDto GetCustomerOrders(GetOrdersCustomerDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<OrderDao>();
                return dao.GetOrdersCustomer(inputDto);
            }
            catch (Exception)
            {
                return this.Output<GetCustomerOrdersDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
