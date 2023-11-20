using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Transaction
{
    public class CreatePaymentBusinessLogic : BaseBusinessLogic
    {
        public CreatePaymentBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public CreateTransactionDaoOutputDto CreateTransaction(CreateTransaction inputDto)
        {
            try
            {
                var dao = this.CreateDao<TransactionDao>();
                inputDto.CreateDate = DateTime.Now;
                inputDto.ExpiredDate = inputDto.CreateDate.Value.AddMinutes(15);
                if (inputDto.TransactionType == 0) inputDto.Status = 0;
                else inputDto.Status = 1;
                var output = dao.Create(inputDto);
                return output;
            }
            catch (Exception)
            {
                return this.Output<CreateTransactionDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
