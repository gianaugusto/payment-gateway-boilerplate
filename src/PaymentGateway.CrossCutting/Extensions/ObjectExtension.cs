namespace PaymentGateway.CrossCutting.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object str)
        {
            return str == null;
        }
    }
}
