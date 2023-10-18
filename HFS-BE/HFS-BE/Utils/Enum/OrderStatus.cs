namespace HFS_BE.Utils.Enum
{
    public static class OrderStatus
    {
        public enum OrderStatusEnum
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
                case (int)OrderStatusEnum.Requested:
                    return "Requested";
                case (int)OrderStatusEnum.Preparing:
                    return "Preparing";
                case (int)OrderStatusEnum.Wait_Shipper:
                    return "Wait Shipper";
                case (int)OrderStatusEnum.Shipping:
                    return "Shipping";
                case (int)OrderStatusEnum.Completed:
                    return "Completed";
                case (int)OrderStatusEnum.InCompleted:
                    return "InCompleted";
                case (int)OrderStatusEnum.Cancel:
                    return "Cancel";
                default:
                    return "unknown";
            }
        }
    }
}
