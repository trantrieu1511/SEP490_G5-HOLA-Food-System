using static HFS_BE.Utils.Enum.VoucherStatusEnum;

namespace HFS_BE.Utils.Enum
{
    public static class NotificationTypeEnum
    {
        public enum NotificationType
        {
            System = 0,
            Shipper_Request = 1
        }

        public static string GetNotifyString(int type)
        {
            switch (type)
            {
                case (int)NotificationType.System:
                    return "System";
                case (int)NotificationType.Shipper_Request:
                    return "Shipper Request";
                default:
                    return "unknown";
            }
        }

        public static int GetNotifyValue(string type)
        {
            switch (type)
            {
                case "System":
                    return (int)NotificationType.System;
                case "Shipper Request":
                    return (int)NotificationType.Shipper_Request;
                default:
                    return -1;
            }
        }
    }
}
