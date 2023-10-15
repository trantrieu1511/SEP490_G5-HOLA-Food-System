using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderProgressBusinessLogic : BaseBusinessLogic
    {
        public OrderProgressBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto CreateOrderProgress(OrderProgressDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<OrderProgressDao>();


                BaseOutputDto baseOutputDto = dao.CreateOrderProgress(inputDto);
                return baseOutputDto;
                
               
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
