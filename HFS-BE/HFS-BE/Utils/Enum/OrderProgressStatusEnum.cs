namespace HFS_BE.Utils.Enum
{
    public static class OrderProgressStatusEnum
    {
        public enum OrderProgressStatus
        {
            Requested = 0,
            Preparing = 1,
            Wait_Shipper = 2,
            Shipping = 3,
            Completed = 4,
            InCompleted = 5,
            Cancel = 6,
        }

        public static string GetOrderProgressStatusString(byte? status)
        {
            switch (status)
            {
                case (int)OrderProgressStatus.Requested:
                    return "Requested";
                case (int)OrderProgressStatus.Preparing:
                    return "Preparing";
                case (int)OrderProgressStatus.Wait_Shipper:
                    return "Wait Shipper";
                case (int)OrderProgressStatus.Shipping:
                    return "Shipping";
                case (int)OrderProgressStatus.Completed:
                    return "Completed";
                case (int)OrderProgressStatus.InCompleted:
                    return "InCompleted";
                case (int)OrderProgressStatus.Cancel:
                    return "Cancel";
                default:
                    return "unknown";
            }
        }
    }
}
