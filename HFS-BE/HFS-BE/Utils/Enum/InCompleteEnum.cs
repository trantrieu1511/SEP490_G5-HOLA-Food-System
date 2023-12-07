namespace HFS_BE.Utils.Enum
{
    public static class InCompleteEnum
    {
        public enum Note
        {
            Note1 = 1,
            Note2 = 2,
            Note3 = 3,
            Note4 = 4

        }

        public static string GetStatusString(string status)
        {
            switch (Int32.Parse(status))
            {
                case (int)Note.Note1:
                    return "Customers don't answer the phone";
                case (int)Note.Note2:
                    return "The customer does not accept the order";
                case (int)Note.Note3:
                    return "The order does not match the customer's requirements";
                case (int)Note.Note4:
                    return "Delivery time is too long";
                default:
                    return "unknown";
            }
        }
    }
}
