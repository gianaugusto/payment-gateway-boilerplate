namespace PaymentGateway.Application.Features.PaymentProcessor.Queries
{
    using MediatR;

    using Models;

    using PaymentGateway.CrossCutting;

    public class GetPaymentById : IRequest<Response<Payment>>
    {
        public Guid PaymentId { get; set; }

        public Guid MerchantId { get; set; }
        
    }
}
