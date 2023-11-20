using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Twilio.Rest.Trunking.V1;

namespace HFS_BE.BusinessLogic.Transaction
{
    public class GetTransactionHistoryBusinessLogic : BaseBusinessLogic
    {
        public GetTransactionHistoryBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetTransactionHistoryDaoOutputDto GetTransactionHistory(GetTransactionHistoryDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<TransactionDao>();
                var output = dao.GetTransactionList(inputDto);
                return output;
            }
            catch (Exception)
            {
                return this.Output<GetTransactionHistoryDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
