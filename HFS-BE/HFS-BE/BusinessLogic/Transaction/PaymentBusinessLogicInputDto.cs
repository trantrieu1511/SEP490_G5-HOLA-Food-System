using HFS_BE.Base;

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
        public string? Note { get; set; }
    }
    public class DashboardAccountantInputDto
    {
        public List<DateTime>? Dates { get; set; } = new List<DateTime>();
        public DateTime? DateFrom { get; set; }
        public DateTime? DateEnd { get; set; }
    }

    public class DashboardAccountantOutputDto : BaseOutputDto
    {
        public List<decimal> MoneyOuts { get; set; } = new List<decimal>();
        public int? totalWithdrawalRequest { get; set; }
        public int? totalSuccessRequest { get; set; }
        public int? totalWaitingRequest { get; set; }
        public int? totalRejectRequest { get; set; }
        public decimal? totalMoneyOut { get; set; }
    }
}
