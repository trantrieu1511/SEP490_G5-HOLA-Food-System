namespace HFS_BE.BusinessLogic.Transaction
{
    public class UpdateTransactionStatusInputDto
    {
        public string? UserId { get; set; }
        public int? TransactionId { get; set; }
        public byte? Status { get; set; }
        public decimal? Value { get; set; }
    }

    public class CreateTransferInputDto
    {
        public string? SenderId { get; set; }
        public string? RecievierId { get; set; }
        public byte? TransactionType { get; set; }
        public decimal? Value { get; set; }
        public string? Code { get; set; }
    }

    public class CreateWithdrawInputDto
    {
        public string? UserId { get; set; }
        public decimal? Value { get; set; }
        public string? Code { get; set; }
    }
}
