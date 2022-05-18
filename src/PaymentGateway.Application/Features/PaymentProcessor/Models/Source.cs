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
            string billingAddress):this()
        {
            SourceId = sourceId;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            Last4 = last4;
            Issuer = issuer;
            BillingAddress = billingAddress;
        }

        private protected Source(string sourceId,
            int expiryMonth,
            int expiryYear,
            string last4,
            string issuer,
            string billingAddress,
            string token) : this(sourceId,
            expiryMonth,
            expiryYear,
            last4,
            issuer,
            billingAddress)
        {
            Token = token;
        }

        public string SourceId { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string Last4 { get; set; }

        public string Issuer { get; set; }

        public string BillingAddress { get; set; }

        public string Token { get; set; }

    }
}
