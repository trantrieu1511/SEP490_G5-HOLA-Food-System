using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using static HFS_BE.Utils.Enum.TransactionTypeEnum;

namespace HFS_BE.DAO.TransantionDao
{
    public class TransactionDao : BaseDao
    {
        public TransactionDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public CreateTransactionDaoOutputDto Create(CreateTransaction inputDto)
        {
            try
            {
                var transactionhistory = new TransactionHistory()
                {
                    SenderId = inputDto.UserId,
                    RecieverId = inputDto.RecieverId,
                    TransactionType = GetStatusString((byte)inputDto.TransactionType),
                    Value = inputDto.Value.Value,
                    CreateDate = inputDto.CreateDate,
                    ExpiredDate = inputDto.ExpiredDate,
                    Status = inputDto.Status
                };

                context.Add(transactionhistory);
                context.SaveChanges();
                var output = this.Output<CreateTransactionDaoOutputDto>(Constants.ResultCdSuccess);
                output.TransactionId = transactionhistory.TransactionId;
                return output;
            }
            catch (Exception)
            {
                return this.Output<CreateTransactionDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
