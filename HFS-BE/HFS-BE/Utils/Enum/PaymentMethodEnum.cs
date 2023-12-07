namespace HFS_BE.Utils.Enum
{
    public static class PaymentMethodEnum
    {
        public enum PaymentMethod
        {
            COD = 0,
            Wallet = 1,
        }

        public static string GetPaymentMethodString(byte? status)
        {
            switch (status)
            {
                case (int)PaymentMethod.COD:
                    return "COD";
                case (int)PaymentMethod.Wallet:
                    return "Wallet";
                default:
                    return "unknown";
            }
        }
    }
}
