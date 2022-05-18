namespace PaymentGateway.CrossCutting.Extensions
{
    public static class StringExtension
    {
        public static bool IsNull(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
