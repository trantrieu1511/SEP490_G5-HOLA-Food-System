using static HFS_BE.Utils.Enum.VoucherStatusEnum;

namespace HFS_BE.Utils.Enum
{
    public static class NotificationTypeEnum
    {
        public enum NotificationType
        {
            System = 0,
            Individual = 1
        }

        public static string GetNotifyString(int type)
        {
            switch (type)
            {
                case (int)NotificationType.System:
                    return "System";
                case (int)NotificationType.Individual:
                    return "Individual";
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
                case "Individual":
                    return (int)NotificationType.Individual;
                default:
                    return -1;
            }
        }
    }
}
