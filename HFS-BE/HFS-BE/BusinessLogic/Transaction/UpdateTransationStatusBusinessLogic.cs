﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Transaction
{
    public class UpdateTransationStatusBusinessLogic : BaseBusinessLogic
    {
        public UpdateTransationStatusBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto UpdateTransationStatus(UpdateTransactionStatusInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<TransactionDao>();
                var input1 = new UpdateTransationStatusDaoInputDto()
                {
                    TransactionId = inputDto.TransactionId,
                    Status = inputDto.Status,
                };

                var output = dao.UpdateTransactionStatus(input1);

                if (inputDto.Status == 1)
                {
                    var input2 = new UpadateWalletBalanceDaoInputDto()
                    {
                        UserId = inputDto.UserId,
                        Value = inputDto.Value,
                    };

                    var output2 = dao.UpdateWalletBalance(input2);
                }

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}