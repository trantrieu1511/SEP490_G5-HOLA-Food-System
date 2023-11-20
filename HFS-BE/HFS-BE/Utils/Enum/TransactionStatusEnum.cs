namespace HFS_BE.Utils.Enum
{
    public class TransactionStatusEnum
    {
        public enum TransactionStatus
        {
            Waiting = 0,
            Success = 1,
            Cancel = 2,
        }

        public static string GetStatusString(byte? status)
        {
            switch (status)
            {
                case (int)TransactionStatus.Waiting:
                    return "Waiting";
                case (int)TransactionStatus.Success:
                    return "Success";
                case (int)TransactionStatus.Cancel:
                    return "Cancel";
                default:
                    return "unknown";
            }
        }
    }
}
