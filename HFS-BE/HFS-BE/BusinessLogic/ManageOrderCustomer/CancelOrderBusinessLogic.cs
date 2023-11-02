﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using static HFS_BE.Utils.Enum.OrderStatusEnum;

namespace HFS_BE.BusinessLogic.ManageOrderCustomer
{
    public class CancelOrderBusinessLogic : BaseBusinessLogic
    {
        public CancelOrderBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CancelOrder(OrderCustomerDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<OrderDao>();
                var getOrder = dao.GetOrderCustomer(inputDto);
                if (getOrder == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Order not exsit!");
                }
                
                if (getOrder.Status == 3 || getOrder.Status == 4 || getOrder.Status == 5)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Can't Cancel order!");
                }

                return dao.CancelOrderCustomer(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
