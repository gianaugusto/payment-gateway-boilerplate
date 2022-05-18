namespace PaymentGateway.Application.Features.PaymentProcessor.Domain.Interfaces
{
    using PaymentProcessor.Models.PaymentIssuer;

    public interface IBankApiClient
    {
        Task<Response> ProcessPaymentAsync(string id, CancellationToken cancellationToken);

        Task<Response> GetPaymentByIdAsync(string id, CancellationToken cancellationToken);
    }
}
