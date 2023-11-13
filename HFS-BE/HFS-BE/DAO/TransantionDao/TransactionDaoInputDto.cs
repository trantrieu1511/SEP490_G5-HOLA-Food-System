namespace HFS_BE.DAO.TransantionDao
{
    public class CreateTransaction
    {
        public string? UserId { get; set; }
        public string? RecieverId { get; set; }
        public int? TransactionType { get; set; }
        public decimal? Value { get; set; }
    }
}
