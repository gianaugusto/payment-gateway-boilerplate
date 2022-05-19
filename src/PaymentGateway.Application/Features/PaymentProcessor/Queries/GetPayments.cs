namespace PaymentGateway.Application.Features.PaymentProcessor.Queries
{
    using MediatR;

    using Models;

    using PaymentGateway.CrossCutting;
    

    public class GetPayments : IRequest<Response<IEnumerable<Payment>>>
    {
        public Guid MerchantId { get; set; }

    }
}
