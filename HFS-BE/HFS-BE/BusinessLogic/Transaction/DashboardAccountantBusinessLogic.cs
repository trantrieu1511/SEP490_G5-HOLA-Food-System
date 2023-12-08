using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.BusinessLogic.Transaction
{
    public class DashboardAccountantBusinessLogic : BaseBusinessLogic
    {
        public DashboardAccountantBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public DashboardAccountantOutputDto GetDashBoard(DashboardAccountantInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<TransactionDao>();
                var input = new GetTransactionHistoryDaoInputDto()
                {
                    DateFrom = inputDto.DateFrom,
                    DateTo = inputDto.DateEnd,
                };

                var output = dao.GetWithdrawRequest(input);
                if (!output.Success)
                {
                    return this.Output<DashboardAccountantOutputDto>(Constants.ResultCdFail);
                }

                var output1 = this.Output<DashboardAccountantOutputDto>(Constants.ResultCdSuccess);
                output1.totalWithdrawalRequest = output.ListTransactions.Count();
                output1.totalRejectRequest = output.ListTransactions.Where(x => x.Status == 2).Count();
                output1.totalWaitingRequest = output.ListTransactions.Where(x => x.Status == 0).Count();
                output1.totalSuccessRequest = output.ListTransactions.Where(x => x.Status == 1).Count();
                output1.totalMoneyOut = output.ListTransactions.Where(x => x.Status == 1).Sum(x => x.Value);

                foreach (var item in inputDto.Dates)
                {
                    var data = output.ListTransactions
                        .FirstOrDefault(x => x.Status == 1
                        && x.UpdateDate.Value.Date == item);
                    if (data == null)
                    {
                        output1.MoneyOuts.Add(0);
                        continue;
                    }

                    output1.MoneyOuts.Add(data.Value);
                }

                return output1;
            }
            catch (Exception)
            {
                return this.Output<DashboardAccountantOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
