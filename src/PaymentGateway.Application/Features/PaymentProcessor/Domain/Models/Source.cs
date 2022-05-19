namespace PaymentGateway.Application.Features.PaymentProcessor.Domain.Models
{
    public class Source
    {
        public static readonly string TableName = nameof(Source);

        public Source() { }

        public Source(Guid sourceId,
            int expiryMonth,
            int expiryYear,
            string last4,
            string fingerprint,
            string issuer,
            string billingAddress) : this()
        {
            SourceId = sourceId;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            Last4 = last4;
            Fingerprint = fingerprint;
            Issuer = issuer;
            BillingAddress = billingAddress;
        }


        public Guid SourceId { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string Last4 { get; set; }

        public string? Fingerprint { get; set; } 

        public string Issuer { get; set; }

        public string? BillingAddress { get; set; }
    }
}
