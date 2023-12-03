using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Transaction
{
    public class UpdateStatusWithdrawRequestBusinessLogic : BaseBusinessLogic
    {
        public UpdateStatusWithdrawRequestBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto UpdateStatusWithDrawRequest(UpdateTransationStatusDaoInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<TransactionDao>();
                var output = dao.UpdateTransactionStatusAccountant(inputDto);
                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
