using HFS_BE.Base;

namespace HFS_BE.DAO.TransantionDao
{
    public class TransactionDaoOutputDto
    {
    }

    public class PaymentReturnOutputDto : BaseOutputDto
    {
        public string VNPayTranID { get; set; }
        public decimal Value { get; set; }
        public string BankCode { get; set; }
    }
}
