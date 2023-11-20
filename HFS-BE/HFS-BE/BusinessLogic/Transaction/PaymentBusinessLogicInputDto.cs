namespace HFS_BE.BusinessLogic.Transaction
{
    public class UpdateTransactionStatusInputDto
    {
        public string? UserId { get; set; }
        public int? TransactionId { get; set; }
        public byte? Status { get; set; }
        public decimal? Value { get; set; }
    }
}
