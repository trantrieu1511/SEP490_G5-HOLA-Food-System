using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Twilio.Rest.Trunking.V1;
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
                    Note = inputDto.Note,
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

        public BaseOutputDto UpdateTransactionStatus(UpdateTransationStatusDaoInputDto inputDto)
        {
            try
            {
                var transaction = this.context.TransactionHistories.FirstOrDefault(x => x.TransactionId == inputDto.TransactionId);
                if (transaction != null)
                {
                    transaction.Status = inputDto.Status;
                    this.context.Update(transaction);
                    this.context.SaveChanges();
                }

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto UpdateWalletBalance(UpadateWalletBalanceDaoInputDto inputDto)
        {
            try
            {
                var user = this.context.Customers.FirstOrDefault(x => x.CustomerId.Equals(inputDto.UserId));
                if (user != null)
                {
                    user.WalletBalance = user.WalletBalance == null ? inputDto.Value : user.WalletBalance + inputDto.Value;
                    this.context.Update(user);
                    this.context.SaveChanges();
                }

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public GetTransactionHistoryDaoOutputDto GetTransactionList(GetTransactionHistoryDaoInputDto inputDto)
        {
            try
            {
                var transactions = this.context.TransactionHistories
                                    .Where(x => (x.SenderId.Equals(inputDto.UserId) || x.RecieverId.Equals(inputDto.UserId))
                                                 && (x.CreateDate == null || (x.CreateDate.Value.Date >= inputDto.DateFrom && x.CreateDate.Value.Date <= inputDto.DateTo)))
                                    .OrderByDescending(x => x.CreateDate)
                                    .ToList();
                foreach (var item in transactions)
                {
                    if (item.ExpiredDate != null && DateTime.Now > item.ExpiredDate)
                    {
                        item.Status = 3;
                    }
                }

                this.context.UpdateRange(transactions);
                this.context.SaveChanges(true);

                var output = this.Output<GetTransactionHistoryDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListTransactions = mapper.Map<List<TransactionHistory>, List<GetTransactionHistoryDaoDto>>(transactions);
                return output;
            }
            catch (Exception)
            {
                return this.Output<GetTransactionHistoryDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
