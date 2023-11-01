namespace HFS_BE.Utils.Enum
{
    public static class PostMenuStatusEnum
    {
        public enum PostMenuStatus
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
                case (int)PostMenuStatus.NotApproved:
                    return "NotApproved";
                case (int)PostMenuStatus.Display:
                    return "Display";
                case (int)PostMenuStatus.Hidden:
                    return "Hidden";
                case (int)PostMenuStatus.Ban:
                    return "Ban";
                default:
                    return "unknown";
            }
        }
    }
}
