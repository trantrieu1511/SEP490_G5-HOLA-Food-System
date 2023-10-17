﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.OrderProgressDao
{
    public class OrderProgressDao : BaseDao
    {
        public OrderProgressDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto CreateOrderProgress(OrderProgressDaoInputDto inputDto)
        {
            try
            {
                var order = mapper.Map<OrderProgressDaoInputDto, OrderProgress>(inputDto);

                context.OrderProgresses.Add(order);
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }


    }
}
