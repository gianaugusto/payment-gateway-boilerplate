namespace PaymentGateway.Application.Features.PaymentProcessor.Models.PaymentIssuer
{
    public class Request
    {
        public Request() { }
        
        public Guid MerchantId { get; set; }

        public string PaymentType { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public string SuccessUrl { get; set; } = string.Empty;

        public string FailureUrl { get; set; } = string.Empty;

        public Source Source { get; set; }
    }
}
