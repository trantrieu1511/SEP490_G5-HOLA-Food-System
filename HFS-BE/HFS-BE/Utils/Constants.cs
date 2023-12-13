namespace HFS_BE.Utils
{
    public static class Constants
    {
        public const bool ResultCdSuccess = true;
        public const bool ResultCdFail = false;
        //post
        public const int PostContentMaxLength = 1500;
        // refresh token
        public const int RefreshTokenLength = 64;
        public const int DayExpiredRefreshToken = 7;
        // order status
        public const int OrderInternal = 0;
        public const int OrderExternal = 1;
        // order progress
        public const int Requested = 0;
        public const int Preparing = 1;
        public const int Wait_Shipper = 2;
        public const int Shipping = 3;
        public const int Completed = 4;
        public const int InCompleted = 5;
        public const int Cancel = 6;

        public static string GetConstantNameFromValue(int value)
        {
            Type constantsType = typeof(Constants);
            foreach (var field in constantsType.GetFields())
            {
                if (field.GetValue(null).Equals(value))
                {
                    return field.Name;
                }
            }
            return null; // Value not found in constants
        }
    }
}
