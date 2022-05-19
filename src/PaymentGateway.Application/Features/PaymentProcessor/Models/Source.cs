namespace PaymentGateway.Application.Features.PaymentProcessor.Models
{
    public class Source
    {
        public Source() { }

        private protected Source(string sourceId, 
            int expiryMonth,
            int expiryYear,
            string last4,
            string issuer,
            string billingAddress,
            bool saveCardInfo) : this()
        {
            SourceId = sourceId;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            Last4 = last4;
            Issuer = issuer;
            BillingAddress = billingAddress;
            SaveCardInfo = saveCardInfo;
        }

        private protected Source(string sourceId,
            int expiryMonth,
            int expiryYear,
            string last4,
            string issuer,
            string billingAddress,
            string token,
            bool saveCardInfo) : this(sourceId,
            expiryMonth,
            expiryYear,
            last4,
            issuer,
            billingAddress,
            saveCardInfo)
        {
            Token = token;
        }

        public string SourceId { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string Last4 { get; set; }

        public string Issuer { get; set; }

        public string Fingerprint { get; set; }

        public string BillingAddress { get; set; }

        public string Token { get; set; }

        public bool SaveCardInfo { get; set; }

    }
}
