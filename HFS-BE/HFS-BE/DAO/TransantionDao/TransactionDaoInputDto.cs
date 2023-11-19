﻿namespace HFS_BE.DAO.TransantionDao
{
    public class CreateTransaction
    {
        public string? UserId { get; set; }
        public string? RecieverId { get; set; }
        public int? TransactionType { get; set; }
        public decimal? Value { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public byte? Status { get; set; }
    }

    public class CreateTransactionDaoInputDto
    {
        public string? UserId { get; set; }
        public string? RecieverId { get; set; }
        public int? TransactionType { get; set; }
        public decimal? Value { get; set; }
    }

    public class PaymentReturnInputDto
    {
        public string? vnp_Amount { get; set; }
        public string? vnp_BankCode { get; set; }
        public string? vnp_BankTranNo { get; set; }
        public string? vnp_CardType { get; set; }
        public string? vnp_OrderInfo { get; set; }
        public string? vnp_PayDate { get; set; }
        public string? vnp_ResponseCode { get; set; }
        public string? vnp_SecureHash { get; set; }
        public string? vnp_TmnCode { get; set; }
        public string? vnp_TransactionNo { get; set; }
        public string? vnp_TransactionStatus { get; set; }
        public string? vnp_TxnRef { get; set; }
    }
}
