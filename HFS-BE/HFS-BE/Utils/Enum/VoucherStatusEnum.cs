namespace HFS_BE.Utils.Enum
{
    public static class VoucherStatusEnum
    {
        public enum VoucherStatus
        {
            Display = 1,
            Hidden = 0
        }

        public static string GetStatusString(byte? status)
        {
            switch (status)
            {
                case (int)VoucherStatus.Display:
                    return "Display";                             
                default:
                    return "Hidden";
            }
        }
    }
}
