namespace PaymentGateway.Application.Features.PaymentProcessor.Domain.Models
{
    using PaymentGateway.CrossCutting.Enums;

    using System;

    public class Payment
    {
        public static readonly string TableName = nameof(Payment);

        public Guid PaymentId { get; set; }

        public Guid MerchantId { get; set; }

        public Guid SourceId { get; set; }

        public DateTime CreateDate { get; set; }

        public string IssuerPaymentId { get; set; }

        public string PaymentType { get; set; }
        
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public Guid CustomerId { get; set; }

        public virtual Merchant Merchant { get; set; }

        public virtual Source Source { get; set; }

        public PaymentStatusEnum Status { get; set; }

        public void ClearCardInfo()
        {
            this.Source.ExpiryYear = default;
            this.Source.ExpiryMonth = default;
            this.Source.Fingerprint = default;
            this.Source.BillingAddress = string.Empty;
            this.Source.Last4 = string.Empty;
        }
    }
}
