namespace HFS_BE.Utils.Enum
{
    public static class PostMenuStatus
    {
        public enum PostMenuStatusEnum
        {
            NotApproved = 0,
            Display = 1,
            Hidden = 2,
            Ban = 3
        }

        public static string GetStatusString(byte? status)
        {
            switch (status)
            {
                case (int)PostMenuStatusEnum.NotApproved:
                    return "NotApproved";
                case (int)PostMenuStatusEnum.Display:
                    return "Display";
                case (int)PostMenuStatusEnum.Hidden:
                    return "Hidden";
                case (int)PostMenuStatusEnum.Ban:
                    return "Ban";
                default:
                    return "unknown";
            }
        }
    }
}
