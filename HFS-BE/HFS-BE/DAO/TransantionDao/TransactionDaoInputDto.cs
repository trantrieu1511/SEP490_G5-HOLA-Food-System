namespace HFS_BE.DAO.TransantionDao
{
    public class CreateTransaction
    {
        public string? UserId { get; set; }
        public string? RecieverId { get; set; }
        public int? TransactionType { get; set; }
        public decimal? Value { get; set; }
    }

    public class PaymentReturnInputDto
    {
        public string? ReturnUrl { get; set; }
    }
}
