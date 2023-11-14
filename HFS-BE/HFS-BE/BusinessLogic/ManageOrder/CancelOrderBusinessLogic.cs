using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.BusinessLogic.ManageOrder
{
    public class CancelOrderBusinessLogic : BaseBusinessLogic
    {
        public CancelOrderBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CancelOrder(OrderProgressCancelInputDto input)
        {
            try
            {
                /*input.UserId = "SE00000001";*/
                var daoOProgress = CreateDao<OrderProgressDao>();
                //get orderprogress by inputDto.orderId
                var orderProgresses = daoOProgress.GetOrderProgresByOrderId(input.OrderId);
                //check orderprogress exist or not
                if (orderProgresses.FirstOrDefault(x => x.Status == input.Status) != null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Cancel Failed", $"OrderId: {input.OrderId} has been cancelled");

                var output = daoOProgress.AddOrderProgressCancelStatus(input);
                return output;
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
