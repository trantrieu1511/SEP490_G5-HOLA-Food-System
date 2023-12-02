namespace HFS_BE.Utils.Enum
{
    public class TransactionTypeEnum
    {
        public enum TransactionType
        {      
            Deposit = 0,
            Withdraw = 1,
            Transfer = 2,
            OrderPaid = 3,
            OrderRefund = 4,
        }

        public static string GetStatusString(byte? status)
        {
            switch (status)
            {
                case (int)TransactionType.Deposit:
                    return "Deposit";
                case (int)TransactionType.Withdraw:
                    return "Withdraw";
                case (int)TransactionType.Transfer:
                    return "Transfer";
                case (int)TransactionType.OrderPaid:
                    return "OrderPaid";
                case (int)TransactionType.OrderRefund:
                    return "OrderRefund";
                default:
                    return "unknown";
            }
        }
    }
}
