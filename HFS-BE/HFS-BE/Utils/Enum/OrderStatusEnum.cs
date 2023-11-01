namespace HFS_BE.Utils.Enum
{
    public static class OrderStatusEnum
    {
        public enum OrderStatus
        {
            Requested = 0,
            Preparing = 1,
            Wait_Shipper = 2,
            Shipping = 3,
            Completed = 4,
            InCompleted = 5,
            Cancel = 6,
        }

        public static string GetOrderStatusString(byte? status)
        {
            switch (status)
            {
                case (int)OrderStatus.Requested:
                    return "Requested";
                case (int)OrderStatus.Preparing:
                    return "Preparing";
                case (int)OrderStatus.Wait_Shipper:
                    return "Wait Shipper";
                case (int)OrderStatus.Shipping:
                    return "Shipping";
                case (int)OrderStatus.Completed:
                    return "Completed";
                case (int)OrderStatus.InCompleted:
                    return "InCompleted";
                case (int)OrderStatus.Cancel:
                    return "Cancel";
                default:
                    return "unknown";
            }
        }
    }
}
