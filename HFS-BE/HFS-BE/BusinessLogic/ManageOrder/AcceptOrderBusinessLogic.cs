using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class AcceptOrderBusinessLogic : BaseBusinessLogic
    {
        public AcceptOrderBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto AcceptOrder(OrderProgressStatusInputDto input)
        {
            try
            {
                /*input.UserId = "SE00000001";*/

                var oProgressDao = CreateDao<OrderProgressDao>();
                //get orderprogress by inputDto.orderId
                var data = oProgressDao.GetOrderProgresByOrderId(input.OrderId);
                // check trong list trả về có status = inputDto.status ko
                if (data.FirstOrDefault(x => x.Status == input.Status) != null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail);
                }
                
                return oProgressDao.AddOrderProgressCommonStatus(input);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
