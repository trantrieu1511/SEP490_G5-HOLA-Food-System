namespace HFS_BE.Utils.Enum
{
    public static class PostStatus
    {
        public enum PostStatusEnum
        {
            NotApproved = 0,
            Display = 1,
            Hidden = 2,
            Ban = 3
        }

        public static string GetPostStatusString(byte? status)
        {
            switch (status)
            {
                case (int)PostStatusEnum.NotApproved:
                    return "NotApproved";
                case (int)PostStatusEnum.Display:
                    return "Display";
                case (int)PostStatusEnum.Hidden:
                    return "Hidden";
                case (int)PostStatusEnum.Ban:
                    return "Ban";
                default:
                    return "unknown";
            }
        }
    }
}
