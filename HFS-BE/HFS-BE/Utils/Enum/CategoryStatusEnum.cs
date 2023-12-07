namespace HFS_BE.Utils.Enum
{
    public static class CategoryStatusEnum
    {
        public enum CategoryStatus
        {
            Display = 1,
            Hidden = 0
        }

        public static string GetStatusString(byte? status)
        {
            switch (status)
            {
                case (int)CategoryStatus.Display:
                    return "Display";               
                default:
                    return "Hidden";
            }
        }
    }
}
