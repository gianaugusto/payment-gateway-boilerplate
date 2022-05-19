namespace PaymentGateway.Application.Features.PaymentProcessor.Gateways.Interfaces
{
    using PaymentProcessor.Models.PaymentIssuer;

    public interface IBankApiClient
    {
        /// <summary>
        /// Request new payment processing 
        /// </summary>
        /// <param name="request">Model with information to process payment</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response> ProcessPaymentAsync(Request request, CancellationToken cancellationToken);

        /// <summary>
        /// Get payments already submitted 
        /// </summary>
        /// <param name="id">issuer payment id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Response> GetPaymentByIdAsync(string id, CancellationToken cancellationToken);
    }
}
