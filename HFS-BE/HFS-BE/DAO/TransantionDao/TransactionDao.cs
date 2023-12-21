using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Mailjet.Client.Resources;
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
                    if (inputDto.Status == 1) transaction.Note += "\n- Success";
                    if (inputDto.Status == 2) transaction.Note += "\n- Cancel";
                    transaction.AcceptBy = inputDto.AccountantId;
                    transaction.UpdateDate = DateTime.Now;
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
        public BaseOutputDto UpdateTransactionStatusAccountant(UpdateTransationStatusDaoInputDto inputDto)
        {
            try
            {
                var transaction = this.context.TransactionHistories.FirstOrDefault(x => x.TransactionId == inputDto.TransactionId);
                if (transaction != null)
                {
                    transaction.Status = inputDto.Status;
                    if (inputDto.Status == 1)
                    {
                        transaction.Note += "\n- Accept by " + inputDto.AccountantId;
                        transaction.AcceptBy = inputDto.AccountantId;
                        transaction.UpdateDate = DateTime.Now;
                    }
                    if (inputDto.Status == 2)
                    {
                        transaction.Note += "\n- Reject" + inputDto.Note + "(" + inputDto.AccountantId + ")";
                       transaction.UpdateDate = DateTime.Now;
                    }
                    
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

        public BaseOutputDto UpdateWalletBalanceSeller(UpadateWalletBalanceDaoInputDto inputDto)
        {
            try
            {
                var user = this.context.Sellers.FirstOrDefault(x => x.SellerId.Equals(inputDto.UserId));
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
                var now = DateTime.Now;
                var transactions = this.context.TransactionHistories
                                    .Where(x => (x.SenderId.Equals(inputDto.UserId) || x.RecieverId.Equals(inputDto.UserId))
                                                 && (x.CreateDate == null || (x.CreateDate.Value.Date >= inputDto.DateFrom && x.CreateDate.Value.Date <= inputDto.DateTo)))
                                    .OrderByDescending(x => x.CreateDate)
                                    .ToList();
                foreach (var item in transactions)
                {
                    if (item.ExpiredDate != null && now > item.ExpiredDate && item.Status == 0)
                    {
                        item.Status = 2;
                        item.Note = item.Note + "\n- Expired";
                       
                        item.UpdateDate = now;
                    }
                }

                this.context.UpdateRange(transactions);
                this.context.SaveChanges();

                var output = this.Output<GetTransactionHistoryDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListTransactions = mapper.Map<List<TransactionHistory>, List<GetTransactionHistoryDaoDto>>(transactions);
                return output;
            }
            catch (Exception)
            {
                return this.Output<GetTransactionHistoryDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public GetTransactionHistoryDaoOutputDto GetWithdrawRequest(GetTransactionHistoryDaoInputDto inputDto)
        {
            try
            {
                var now = DateTime.Now;
                var transactions = this.context.TransactionHistories
                                    .Where(x => x.TransactionType.Equals("Withdraw")
                                                 && (x.CreateDate == null || (x.CreateDate.Value.Date >= inputDto.DateFrom && x.CreateDate.Value.Date <= inputDto.DateTo)))
                                    .OrderByDescending(x => x.CreateDate)
                                    .ToList();
                foreach (var item in transactions)
                {
                    if (item.ExpiredDate != null && now > item.ExpiredDate)
                    {
                        item.Status = 2;
                        item.Note = item.Note + "\n- Expired";
                        item.UpdateDate = now;
                    }
                }

                this.context.UpdateRange(transactions);
                this.context.SaveChanges();

                var output = this.Output<GetTransactionHistoryDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListTransactions = mapper.Map<List<TransactionHistory>, List<GetTransactionHistoryDaoDto>>(transactions);
                return output;
            }
            catch (Exception)
            {
                return this.Output<GetTransactionHistoryDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto SaveTransferCode(CreateWalletTransferCodeInputDto inputDto)
        {
            try
            {
                var now = DateTime.Now;
                var userCd = this.context.WalletTransferCodes.FirstOrDefault(x => x.UserId.Equals(inputDto.UserId));
                var output = this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                if (userCd == null)
                {
                    var code = new WalletTransferCode()
                    {
                        UserId = inputDto.UserId,
                        Code = inputDto.Code,
                        ExpiredDate = inputDto.ExpiredDate,
                        IsUsed = false,
                    };

                    this.context.Add(code);
                    this.context.SaveChanges();

                    return output;
                }

                if (userCd.ExpiredDate >= now && userCd.IsUsed == false)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Your last code is not expired! Check your mail!");
                }

                userCd.Code = inputDto.Code;
                userCd.ExpiredDate = inputDto.ExpiredDate;
                userCd.IsUsed = false;
                this.context.Update(userCd);
                this.context.SaveChanges();
                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public WalletTransferCode GetTransferWallet(string userId)
        {
                var transfercode = this.context.WalletTransferCodes.FirstOrDefault(x => x.UserId.Equals(userId));
                return transfercode;
        }

        public BaseOutputDto UpdateStatusUsed(int codeId)
        {
            try
            {
                var transfercode = this.context.WalletTransferCodes.FirstOrDefault(x => x.CodeId == codeId);
                if (transfercode != null)
                {
                    transfercode.IsUsed = true;
                    this.context.Update(transfercode);
                    this.context.SaveChanges();
                }

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public TransactionHistory GetTransactionHistory(int transactionId)
        {
            var data = this.context.TransactionHistories.FirstOrDefault(x => x.TransactionId == transactionId);
            return data;
        }
    }
}
