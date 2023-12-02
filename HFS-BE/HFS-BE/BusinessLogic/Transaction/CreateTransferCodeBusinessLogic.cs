using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Twilio.Rest.Trunking.V1;

namespace HFS_BE.BusinessLogic.Transaction
{
    public class CreateTransferCodeBusinessLogic : BaseBusinessLogic
    {
        public CreateTransferCodeBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CreateTransferCd(CreateWalletTransferCodeInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<TransactionDao>();
                return dao.SaveTransferCode(inputDto);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}
