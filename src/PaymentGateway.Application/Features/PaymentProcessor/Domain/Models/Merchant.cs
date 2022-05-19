namespace PaymentGateway.Application.Features.PaymentProcessor.Domain.Models
{
    public class Merchant
    {
        public static readonly string TableName = nameof(Merchant);

        public Guid MerchantId { get; }

        public string MerchantName { get; } = string.Empty;

        public string SuccessUrl  { get; set; }

        public string FailureUrl { get; set; }

        public string PrivateKey { get; set; }

        public string PublicKey { get; set; }

        public virtual List<Payment> Payments { get; set; }

        public Merchant()
        {
            Payments = new List<Payment>();
        }

        public Merchant(string merchantName) : this()
        {
            MerchantName = merchantName;
        }

        public Merchant(Guid merchantId, string merchantName) : this(merchantName)
        {
            MerchantId = merchantId;
        }

        public Merchant(Guid merchantId,
            string merchantName,
            string successUrl,
            string failureUrl,
            string privateKey,
            string publicKey) : this(merchantId,
            merchantName)
        {
            SuccessUrl = successUrl;
            FailureUrl = failureUrl;
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

    }
}
