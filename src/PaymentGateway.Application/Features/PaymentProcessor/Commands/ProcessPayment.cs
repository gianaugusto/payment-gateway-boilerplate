namespace PaymentGateway.Application.Features.PaymentProcessor.Commands
{
    using CrossCutting;
    using MediatR;
    using Models;

    public class ProcessPayment : IRequest<Response<Payment>>
    {
        public Guid PaymentId { get; set; }

        public Guid MerchantId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public string PaymentType { get; set; }

        public double Amount { get; set; }

        public string Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public Source Source { get; set; }
        
    }
}
