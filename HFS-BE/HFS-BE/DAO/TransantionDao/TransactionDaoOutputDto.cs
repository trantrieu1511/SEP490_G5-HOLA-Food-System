﻿using HFS_BE.Base;

namespace HFS_BE.DAO.TransantionDao
{
    public class CreateTransactionDaoOutputDto : BaseOutputDto
    {
        public int? TransactionId { get; set; }
    }

    public class PaymentReturnOutputDto : BaseOutputDto
    {
        public string VNPayTranID { get; set; }
        public decimal Value { get; set; }
        public string BankCode { get; set; }
    }

    public class GetTransactionHistoryDaoDto
    {
        public int TransactionId { get; set; }
        public string SenderId { get; set; } = null!;
        public string? RecieverId { get; set; }
        public string TransactionType { get; set; } = null!;
        public string? Note { get; set; }
        public decimal Value { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public byte? Status { get; set; }
    }

    public class GetTransactionHistoryDaoOutputDto : BaseOutputDto
    {
        public List<GetTransactionHistoryDaoDto> ListTransactions { get; set; }
    }
}
