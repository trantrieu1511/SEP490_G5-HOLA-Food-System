using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderExternalBusinessLogic : BaseBusinessLogic
    {
        public OrderExternalBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public OrderExternalLstOutputDto GetAllOrderExternalShipper()
        {
            try
            {
                var dao = CreateDao<OrderDao>();
                return dao.GetAllOrderExternalShipper();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
