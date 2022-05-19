namespace PaymentGateway.CrossCutting.Enums
{
    public enum PaymentStatusEnum
    {
        Created = 0,
        Pending = 1,
        Authorized = 2,
        Expired = 3,
        Failed = 4,
        Denied = 5,
        Completed = 6
    }
}
